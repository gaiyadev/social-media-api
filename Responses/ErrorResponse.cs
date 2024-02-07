using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Utils;

namespace SocialMediaApp.Responses;

public class ErrorResponse
{
        public static IActionResult HandleNotFound(string errorMessage)
        {
            var notFoundApiResponse = new ErrorResponseModel
            {
                Message = errorMessage,
                StatusCode = 404,
                Error =  HttpStatusTitles.NotFound,
            };
            return new ObjectResult(notFoundApiResponse)
            {
                StatusCode = 404
            };
        }

        public static IActionResult HandleForbidden(string errorMessage)
        {
            var forbidApiResponse = new ErrorResponseModel
            {
                Message = errorMessage,
                StatusCode = 403,
                Error =  HttpStatusTitles.Forbidden,
            };
            return new ObjectResult(forbidApiResponse)
            {
                StatusCode = 403
            };
        }

        public static IActionResult HandleBadRequest(string errorMessage)
        {
            var badRequestApiResponse = new ErrorResponseModel
            {
                Message = errorMessage,
                StatusCode = 400,
                Error = HttpStatusTitles.BadRequest,
            };
            return new ObjectResult(badRequestApiResponse)
            {
                StatusCode = 400
            };
        }
        
        public static IActionResult HandleConflictException(string errorMessage)
        {
            var conflictResponse = new ErrorResponseModel
            {
                Message = errorMessage,
                StatusCode = 409,
                Error =  HttpStatusTitles.Conflict,
            };
            return new ObjectResult(conflictResponse)
            {
                StatusCode = 409
            };
        }
        
        public static IActionResult HandleUnauthorizedException(string errorMessage)
        {
            var unauthorizedResponse = new ErrorResponseModel
            {
                Message = errorMessage,
                StatusCode = 401,
                Error =  HttpStatusTitles.Unauthorized,
            };
            return new ObjectResult(unauthorizedResponse)
            {
                StatusCode = 409
            };
        }
        public static IActionResult HandleInternalServerError(string errorMessage)
        {
            var internalServerErrorApiResponse = new ErrorResponseModel
            {
                Message = errorMessage,
                StatusCode = 500,
                Error =  HttpStatusTitles.InternalServerError,
            };
            return new ObjectResult(internalServerErrorApiResponse)
            {
                StatusCode = 500
            };
        }
    }