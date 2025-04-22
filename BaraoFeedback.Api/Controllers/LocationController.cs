using BaraoFeedback.Application.DTOs.Location;
using BaraoFeedback.Application.Services.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
[Authorize]
[Route("location/")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;
    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet("get-location/")]
    public async Task<IActionResult> GetLocationAsync(long intitutionId)
    {
        var response = await _locationService.GetLocationAsync(intitutionId);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("get-location-by-id/")]
    public async Task<IActionResult> GetLocationByIdAsync(long locationId)
    {
        var response = await _locationService.GetLocationByIdAsync(locationId);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("post-location/")]
    public async Task<IActionResult> PostLocationAsync(LocationInsertRequest request)
    {
        var response = await _locationService.PostLocationAsync(request);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }
    [HttpDelete("delete-location/")]
    public async Task<IActionResult> DeleteLocationAsync(long institutionId)
    {
        var response = await _locationService.DeleteAsync(institutionId);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    } 
}
