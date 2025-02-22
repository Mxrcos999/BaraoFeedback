using BaraoFeedback.Application.DTOs.User;

namespace BaraoFeedback.Application.Services.User;

public interface IIdentityService
{
    Task<UserLoginResponse> LoginAsync(UserLoginRequest userLogin);
    Task<UserRegisterResponse> RegisterAdminAsync(string type, AdminRegisterRequest request);
    Task<UserRegisterResponse> RegisterStudentAsync(string type, StudentRegisterRequest userRegister);
}
