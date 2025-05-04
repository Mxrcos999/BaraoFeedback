using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.Services.TicketCategory;
using BaraoFeedback.Infra.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
[Authorize]
[Route("category/")]
public class TicketCategoryController : ControllerBase
{
    private readonly ITicketCategoryService ticketCategoryService;
    public TicketCategoryController(ITicketCategoryService ticketCategoryService)
    {
        this.ticketCategoryService = ticketCategoryService;
    }

    [HttpGet]
    [Route("get-ticket-category")]
    public async Task<IActionResult> GetCategoriesAsync([FromQuery] TicketCategoryQuery query)
    {
        var response = await ticketCategoryService.GetTicketCategoryAsync(query);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }
    [HttpGet]
    [Route("get-ticket-category-options")]
    public async Task<IActionResult> GetCategoryAsync()
    {
        var response = await ticketCategoryService.GetCategoryAsync();

        if (response.Count == 0)
            return NoContent();

        return Ok(response);
    }

    [HttpGet]
    [Route("get-category")]
    public async Task<IActionResult> GetCategoryListAsync()
    {
        var response = await ticketCategoryService.GetCategoryListAsync();

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }
    [HttpPost]
    [Route("post-category")]

    public async Task<IActionResult> PostCategoryAsync(TicketCategoryInsertRequest request)
    {
        var response = await ticketCategoryService.InsertTicketCategoryAsync(request);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }
    [HttpDelete("delete-category")]
    public async Task<IActionResult> DeleteInstitutionAsync(long categoryId)
    {
        var response = await ticketCategoryService.DeleteAsync(categoryId);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }
}
