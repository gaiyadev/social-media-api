using BookstoreAPI.CustomResponses.Responses;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Utils;

namespace SocialMediaApp.Responses;

public class ErrorResponse
{
            public static IActionResult HandleNotFound(string errorMessage)
        {
            var notFoundApiResponse = new ErrorResponseModel
            {
                Error = errorMessage,
                StatusCode = 404,
                Title =  HttpStatusTitles.NotFound,
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
                Error = errorMessage,
                StatusCode = 403,
                Title =  HttpStatusTitles.Forbidden,
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
                Error = errorMessage,
                StatusCode = 400,
                Title = HttpStatusTitles.BadRequest,
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
                Error = errorMessage,
                StatusCode = 409,
                Title =  HttpStatusTitles.Conflict,
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
                Error = errorMessage,
                StatusCode = 401,
                Title =  HttpStatusTitles.Unauthorized,
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
                Error = errorMessage,
                StatusCode = 500,
                Title =  HttpStatusTitles.InternalServerError,
            };
            return new ObjectResult(internalServerErrorApiResponse)
            {
                StatusCode = 500
            };
        }
    }