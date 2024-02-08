using Microsoft.AspNetCore.Mvc;

namespace SocialMediaApp.Responses;

public sealed class ErrorResponse
{
        public IActionResult HandleError(string errorMessage, int StatusCode,  string error)
        {
            var notFoundApiResponse = new ErrorResponseModel
            {
                Message = errorMessage,
                StatusCode = StatusCode,
                Error = error,
            };
            return new ObjectResult(notFoundApiResponse)
            {
                StatusCode = StatusCode
            };
        }
    }