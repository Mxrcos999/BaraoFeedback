using BaraoFeedback.Application.DTOs.Shared;
using System.Text.Json.Serialization;

namespace BaraoFeedback.Application.DTOs.User;

public class UserLoginResponse
{

    public bool Success => Errors.Message.Count == 0 ? true : false;

    public string Email { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string AccessToken { get; private set; }
    public string? ExpirationTimeAccessToken { get; private set; }
    public DateTime ExpirationDateTimeAccessToken { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string RefreshToken { get; private set; }
    public string? ExpirationTimeRefreshtoken { get; private set; }
    public DateTime ExpirationDateTimeRefreshtoken { get; private set; }
    public Errors Errors { get; set; } = new Errors();


    public UserLoginResponse(bool success)
    { }

    public UserLoginResponse(bool success, string accessToken, string refreshToken, string expirationTimeRefreshtoken, string expirationTimeAccessToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpirationTimeAccessToken = expirationTimeAccessToken;
        ExpirationTimeRefreshtoken = expirationTimeRefreshtoken;
        ExpirationDateTimeAccessToken = DateTime.Now.AddSeconds(3000);
        ExpirationDateTimeRefreshtoken = DateTime.Now.AddSeconds(10200);
    }

    public UserLoginResponse(bool success, string accessToken, string refreshToken, string email)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Email = email;
    }
}
