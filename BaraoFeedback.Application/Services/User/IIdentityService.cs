using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.User;

namespace BaraoFeedback.Application.Services.User;

public interface IIdentityService
{
    Task<DefaultResponse> DeleteUser(string id);
    Task<bool> UnlockUser(string userId, string token);
    Task<DefaultResponse> GetUsers();
    Task<DefaultResponse> UpdatePasswordAsync(UpdatePassword dto);
    Task<UserLoginResponse> LoginAsync(UserLoginRequest userLogin);
    Task<UserRegisterResponse> RegisterAdminAsync(string type, AdminRegisterRequest request);
    Task<UserRegisterResponse> RegisterStudentAsync(string type, StudentRegisterRequest userRegister);
}
