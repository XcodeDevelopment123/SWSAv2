using Newtonsoft.Json;

namespace SWSA.MvcPortal.Dtos.Responses;

public class LoginResult
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? StaffId { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public LoginResultType Type { get; set; }

    public LoginResult Success(string staffId)
    {
        return new LoginResult
        {
            StaffId = staffId,
            IsSuccess = true,
            Message = "Login successful",
            Type = LoginResultType.Success
        };
    }

    public LoginResult Failed(LoginResultType type)
    {
        // Ensure that Failed() is called with a valid error type
        if (type == LoginResultType.Success)
        {
            throw new ArgumentException("LoginResultType cannot be Success when using Failed() method");
        }

        // Return specific failure message based on the type
        return new LoginResult
        {
            IsSuccess = false,
            Message = GetFailureMessage(type),
            Type = type
        };
    }

    // Get the failure message based on the error type
    private string GetFailureMessage(LoginResultType type)
    {
        return type switch
        {
            LoginResultType.UserNotFound => "The username was not found.",
            LoginResultType.InvalidPassword => "The password is incorrect.",
            _ => "Login failed due to an unknown error."
        };
    }

    public enum LoginResultType
    {
        Success = 0,
        UserNotFound = 1,
        InvalidPassword = 2,
        AccountNotEnable=3,
        Others = 99,
    }

}