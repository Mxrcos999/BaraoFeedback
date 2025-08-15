using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Extensions;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Application.Services.Email;
using BaraoFeedback.Application.DTOs.Querys;
using OfficeOpenXml;
using System.IO;

namespace BaraoFeedback.Application.Services.Ticket;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IEmailService _emailService;

    public TicketService(ITicketRepository ticketRepository, IEmailService emailService)
    {
        _ticketRepository = ticketRepository;
        _emailService = emailService;
    }

    public async Task<BaseResponse<List<TicketResponse>>> GetTicketAsync(TicketQuery query)
    {
        var response = new BaseResponse<List<TicketResponse>>();
        var data = (await _ticketRepository.GetTicketAsync(query))
            .Pagination<TicketResponse>(new BaseGetRequest() { Page = query.Page, PageSize = query.PageSize, SearchInput = query.SearchInput});
        var totalRecord = (await _ticketRepository.GetTicketAsync(query)).Count();
        
        response.TotalRecords = totalRecord;
        response.PageSize = data.Count();
        response.Page = query.Page;  
        response.Data = data.ToList();
        return response;
    }
    public async Task<BaseResponse<TicketResponse>> GetTicketByIdAsync(long id)
    {
        var response = new BaseResponse<TicketResponse>();

        response.Data = await _ticketRepository.GetTicketByIdAsync(id);

        return response;
    }
    public async Task<BaseResponse<bool>> ProcessTicketAsync(long id, bool status)
    {
        var response = new BaseResponse<bool>();

        var ticket = await _ticketRepository.GetByIdAsync(id);

        ticket.Processed = status;

        response.Data = await _ticketRepository.UpdateAsync(ticket, default);

        return response;
    }
    public async Task<BaseResponse<bool>> PostTicketAsync(TicketInsertRequest request)
    {
        var response = new BaseResponse<bool>();
        var entity = new Domain.Entities.Ticket()
        {
            ApplicationUserId = _ticketRepository.GetUserId(),
            Description = request.Description,
            InstitutionId = request.InstitutionId,
            TicketCategoryId = request.CategoryId,
            LocationId = request.LocationId,
            Title = request.Title, 
        };
        var result = await _ticketRepository.PostTicketAsync(entity);
        response.Data = result;

        var ticket = await _ticketRepository.GetTicketByIdAsync(entity.Id);
        await _emailService.SendEmail(ticket);

        return response;
    }

    public async Task<BaseResponse<bool>> DeleteAsync(long entityId)
    {
        var response = new BaseResponse<bool>();
        var entity = await _ticketRepository.GetByIdAsync(entityId);
        response.Data = await _ticketRepository.DeleteAsync(entity, default);

        return response;
    }

    public async Task<byte[]> GenerateTicketReportAsync(TicketQuery query)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        var tickets = await _ticketRepository.GetTicketReportAsync(query);
        var ticketList = tickets.ToList();

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Relatório de Tickets");

        // Cabeçalhos
        worksheet.Cells[1, 1].Value = "ID do Ticket";
        worksheet.Cells[1, 2].Value = "Título";
        worksheet.Cells[1, 3].Value = "Descrição";
        worksheet.Cells[1, 4].Value = "Código do Aluno";
        worksheet.Cells[1, 5].Value = "Nome do Aluno";
        worksheet.Cells[1, 6].Value = "Email do Aluno";
        worksheet.Cells[1, 7].Value = "Instituição";
        worksheet.Cells[1, 8].Value = "Local";
        worksheet.Cells[1, 9].Value = "Categoria";
        worksheet.Cells[1, 10].Value = "Status";
        worksheet.Cells[1, 11].Value = "Data de Criação";

        // Estilizar cabeçalhos
        var headerRange = worksheet.Cells[1, 1, 1, 11];
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        headerRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

        // Dados
        for (int i = 0; i < ticketList.Count; i++)
        {
            var ticket = ticketList[i];
            var row = i + 2;

            worksheet.Cells[row, 1].Value = ticket.TicketId;
            worksheet.Cells[row, 2].Value = ticket.Title;
            worksheet.Cells[row, 3].Value = ticket.Description;
            worksheet.Cells[row, 4].Value = ticket.StudentCode;
            worksheet.Cells[row, 5].Value = ticket.StudentName;
            worksheet.Cells[row, 6].Value = ticket.StudentEmail;
            worksheet.Cells[row, 7].Value = ticket.InstitutionName;
            worksheet.Cells[row, 8].Value = ticket.LocationName;
            worksheet.Cells[row, 9].Value = ticket.CategoryName;
            worksheet.Cells[row, 10].Value = ticket.Status;
            worksheet.Cells[row, 11].Value = ticket.CreatedAt;

            // Estilizar células de status
            if (ticket.Status == "Processado")
            {
                worksheet.Cells[row, 10].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[row, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
            }
            else
            {
                worksheet.Cells[row, 10].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[row, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
            }
        }

        // Auto-dimensionar colunas
        worksheet.Cells.AutoFitColumns();

        // Adicionar bordas
        var dataRange = worksheet.Cells[1, 1, ticketList.Count + 1, 11];
        dataRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        dataRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        dataRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        dataRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        dataRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

        // Adicionar informações do relatório
        worksheet.Cells[ticketList.Count + 3, 1].Value = $"Relatório gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        worksheet.Cells[ticketList.Count + 3, 1].Style.Font.Bold = true;
        worksheet.Cells[ticketList.Count + 4, 1].Value = $"Total de tickets: {ticketList.Count}";
        worksheet.Cells[ticketList.Count + 4, 1].Style.Font.Bold = true;

        return await package.GetAsByteArrayAsync();
    }
}
