using BaraoFeedback.Application.DTOs.User;
using BaraoFeedback.Application.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IIdentityService _userService;
    public UserController(IIdentityService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("user/post-student-user")]
    public async Task<IActionResult> RegisterStudentAsync(StudentRegisterRequest request)
    {
        var response = await _userService.RegisterStudentAsync("student", request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet]
    [Route("user/get-admin-list")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var response = await _userService.GetUsers();
         
        return Ok(response);
    }

    [HttpPatch]
    [Route("user/update-password")]
    public async Task<IActionResult> UpdateChangeAsync(UpdatePassword model)
    {
        var response = await _userService.UpdatePasswordAsync(model);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost]
    [Route("user/post-admin-user")]
    public async Task<IActionResult> RegisterAdminAsync(AdminRegisterRequest request)
    {
        var response = await _userService.RegisterAdminAsync("admin", request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost]
    [Route("user/user-login")]
    public async Task<IActionResult> LoginUserAsync(UserLoginRequest request)
    {
        var response = await _userService.LoginAsync(request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }
}
