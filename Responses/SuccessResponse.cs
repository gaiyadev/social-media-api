using Microsoft.AspNetCore.Mvc;

namespace SocialMediaApp.Responses;

public sealed class SuccessResponse
{    
    public IActionResult HandleSuccess<T>(string message, T data, int StatusCode, string? accessToken)
    {
        var apiResponse = new SuccessResponseModel<T>
        {
            Message = message,
            StatusCode = StatusCode,
            Status = "Success",
            Data = data,
            accessToken = accessToken
        };

        return new ObjectResult(apiResponse)
        {
            StatusCode = StatusCode
        };
    }
    
}