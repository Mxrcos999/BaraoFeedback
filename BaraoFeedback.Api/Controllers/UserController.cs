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

        return Ok(response);
    }

    [HttpPost]
    [Route("user/post-admin-user")]
    public async Task<IActionResult> RegisterAdminAsync(AdminRegisterRequest request)
    {
        var response = await _userService.RegisterAdminAsync("admin", request);

        return Ok(response);
    }
    [HttpPost]
    [Route("user/user-login")]
    public async Task<IActionResult> LoginUserAsync(UserLoginRequest request)
    {
        var response = await _userService.LoginAsync(request);

        return Ok(response);
    }
}
