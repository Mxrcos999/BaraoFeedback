using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Infra.Querys;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
public class TicketCategoryController : ControllerBase
{
    public TicketCategoryController()
    {

    }

    [HttpGet]
    [Route("category/get-ticket-category")]
    public async Task<IActionResult> GetCategoriesAsync([FromQuery] TicketCategoryQuery query)
    {
        return Ok
            (
                new DefaultResponse()
                {
                    Data = new List<CategoryResponse>()
                    {
                        new CategoryResponse()
                        {
                            Description = "Descricao maluca",
                            DescriptionId = 555555,
                            TicketQuantity = 99
                        },   new CategoryResponse()
                        {
                            Description = "Descricao maluca",
                            DescriptionId = 555555,
                            TicketQuantity = 99
                        },   new CategoryResponse()
                        {
                            Description = "Descricao maluca",
                            DescriptionId = 555555,
                            TicketQuantity = 99
                        },   new CategoryResponse()
                        {
                            Description = "Descricao maluca",
                            DescriptionId = 555555,
                            TicketQuantity = 99
                        },
                    }
                }
            );
    }
    //[HttpPost]
    //public async Task<IActionResult> PostCategoryAsync()
    //{
    //    return Ok();
    //}
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
