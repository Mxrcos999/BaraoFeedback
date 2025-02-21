using BaraoFeedback.Application.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IIdentityService _userService;
    public UserController(IIdentityService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("user/post-student-user")]
    public async Task<IActionResult> RegisterStudentAsync(UserRegisterRequest request)
    {
        var response = await _userService.RegisterStudent("student", request);

        return Ok(response);
    }

    [HttpPost]
    [Route("user/post-admin-user")]
    public async Task<IActionResult> RegisterAdminAsync(UserRegisterRequest request)
    {
        var response = await _userService.RegisterStudent("admin", request);

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
