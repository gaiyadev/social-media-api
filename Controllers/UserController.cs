
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.DTOs;
using SocialMediaApp.Exceptions;
using SocialMediaApp.Responses;
using SocialMediaApp.Services;
using SocialMediaApp.Services.User;
using SocialMediaApp.Utils;

namespace SocialMediaApp.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtService _jwtService;

    private readonly SuccessResponse _successResponse;

    private readonly ErrorResponse _errorResponse;

    public UserController(IUserService userService, JwtService jwtService, ErrorResponse errorResponse, SuccessResponse successResponse)
    {
        _userService = userService;
        _jwtService = jwtService;
        _errorResponse = errorResponse;
        _successResponse = successResponse;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
    {
        try
        {
            var user = await _userService.Signup(signUpDto);

            var response = new List<object>
            {
                new { id = user.Id, email = user.Email }
            };
            return _successResponse.HandleSuccess("Successfully created", response, (int)HttpStatusCode.Created, null);
        }
        catch (ConflictException ex)
        {
            return _errorResponse.HandleError(ex.Message, (int)HttpStatusCode.Conflict, HttpStatusTitles.Conflict);
        }
        catch (InternalServerException ex)
        {
            return _errorResponse.HandleError(ex.Message, (int)HttpStatusCode.InternalServerError, HttpStatusTitles.InternalServerError);
        }
        catch (Exception ex)
        {
            return _errorResponse.HandleError(ex.Message, (int)HttpStatusCode.InternalServerError, HttpStatusTitles.InternalServerError);
        }

    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
    {
        try
        {
            var user = await _userService.SignIn(signInDto);

            var token = _jwtService.CreateToken(user.Email, user.FirstName, user.Id);

            var response = new List<object>
            {
                new { id = user.Id, email = user.Email }
            };

            return _successResponse.HandleSuccess("Successfully login", response, (int)HttpStatusCode.OK, token);
        }
        catch (ForbiddenException ex)
        {
            return _errorResponse.HandleError(ex.Message, (int)HttpStatusCode.Forbidden, HttpStatusTitles.Forbidden);

        }
        catch (NotFoundException ex)
        {
            return _errorResponse.HandleError(ex.Message, (int)HttpStatusCode.NotFound, HttpStatusTitles.NotFound);
        }
        catch (Exception ex)
        {
            return _errorResponse.HandleError(ex.Message, (int)HttpStatusCode.InternalServerError, HttpStatusTitles.InternalServerError);
        }
    }

}