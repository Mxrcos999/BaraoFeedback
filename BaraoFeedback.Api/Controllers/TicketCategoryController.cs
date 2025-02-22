using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Services.TicketCategory;
using BaraoFeedback.Infra.Querys;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
public class TicketCategoryController : ControllerBase
{
    private readonly ITicketCategoryService ticketCategoryService;
    public TicketCategoryController(ITicketCategoryService ticketCategoryService)
    {
        this.ticketCategoryService = ticketCategoryService;
    }

    [HttpGet]
    [Route("category/get-ticket-category")]
    public async Task<IActionResult> GetCategoriesAsync([FromQuery] TicketCategoryQuery query)
    {
        var response = await ticketCategoryService.GetTicketCategoryAsync(query);
        return Ok();
    }
    [HttpGet]
    [Route("category/get-category")]
    public async Task<IActionResult> GetCategoryAsync()
    {
        var response = await ticketCategoryService.GetCategoryAsync();

        return Ok(response);
    }
    [HttpPost]
    [Route("category/post-category")]

    public async Task<IActionResult> PostCategoryAsync(TicketCategoryInsertRequest request)
    {
        var response = await ticketCategoryService.InsertTicketCategoryAsync(request);
        return Ok(response);
    }
    //[HttpDelete]
    //public async Task<IActionResult> DeleteCategoryAsync()
    //{
    //    return Ok();
    //}
    //[HttpPut]
    //public async Task<IActionResult> PutCategoriesAsync()
    //{
    //    return Ok();
    //}
}
