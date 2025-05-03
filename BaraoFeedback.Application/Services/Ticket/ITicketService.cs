using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Infra.Querys;
using System.Threading.Tasks;

namespace BaraoFeedback.Application.Services.Ticket;

public interface ITicketService
{
    Task<BaseResponse<bool>> DeleteAsync(long entityId);
    Task<BaseResponse<bool>> ProcessTicketAsync(long id);
    Task<BaseResponse<List<TicketResponse>>> GetTicketAsync(TicketQuery query);
    Task<BaseResponse<TicketResponse>> GetTicketByIdAsync(long id);
    Task<BaseResponse<bool>> PostTicketAsync(TicketInsertRequest request);
}
