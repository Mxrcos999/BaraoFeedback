using BaraoFeedback.Application.DTOs.Institution;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Services.Institution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
[Route("institution/")]
public class InstitutionController : ControllerBase
{
    private readonly IInstitutionService _institutionService;

    public InstitutionController(IInstitutionService institutionService)
    {
        _institutionService = institutionService;
    }

    [HttpGet("get-institution")]
    public async Task<IActionResult> GetInstitutionAsync([FromQuery] BaseGetRequest request)
    {
        var response = await _institutionService.GetInstitutionAsync(request);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("get-institution-options")]
    public async Task<IActionResult> GetInstitutionOptionAsync()
    {
        var response = await _institutionService.GetInstitutionOptionsAsync();

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

    [HttpPut("update-institution")]
    public async Task<IActionResult> DeleteInstitutionAsync([FromQuery] long institutionId, [FromBody] InstitutionUpdateRequest model)
    {
        var response = await _institutionService.UpdateAsync(institutionId, model);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }
}
