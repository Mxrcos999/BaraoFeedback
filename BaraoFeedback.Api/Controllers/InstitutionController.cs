using BaraoFeedback.Application.DTOs.Institution;
using BaraoFeedback.Application.Services.Institution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
[Authorize]
[Route("institution/")]
public class InstitutionController : ControllerBase
{
    private readonly IInstitutionService _institutionService;

    public InstitutionController(IInstitutionService institutionService)
    {
        _institutionService = institutionService;
    }

    [HttpGet("get-institution")]
    public async Task<IActionResult> GetInstitutionAsync()
    {
        var response = await _institutionService.GetInstitutionAsync();

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }
    [HttpPost("post-institution")]
    public async Task<IActionResult> PostInstitutionAsync(InstitutionInsertRequest request)
    {
        var response = await _institutionService.PostInstitutionAsync(request);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }
    [HttpDelete("delete-institution")]
    public async Task<IActionResult> DeleteInstitutionAsync(long institutionId)
    {
        var response = await _institutionService.DeleteAsync(institutionId);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }
}
