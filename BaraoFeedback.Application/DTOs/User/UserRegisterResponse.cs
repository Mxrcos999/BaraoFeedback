using BaraoFeedback.Application.DTOs.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BaraoFeedback.Application.DTOs.User;

public sealed class UserRegisterResponse
{
    public bool Success => Errors.Message.Count == 0 ? true : false;

    public Errors Errors { get; set; } = new Errors();
    public string Data { get; set; }


    public UserRegisterResponse(bool success)
    {
    }
}
