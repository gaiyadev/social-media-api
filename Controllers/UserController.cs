
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.DTOs;
using SocialMediaApp.Exceptions;
using SocialMediaApp.Responses;
using SocialMediaApp.Services;
using SocialMediaApp.Services.User;

namespace SocialMediaApp.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtService _jwtService;

    public UserController(IUserService userService, JwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
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

            return SuccessResponse.HandleCreated("Successfully created", response);
        }
        catch (ConflictException ex)
        {
            return ErrorResponse.HandleConflictException(ex.Message);
        }
        catch (InternalServerException ex)
        {
            return ErrorResponse.HandleInternalServerError(ex.Message);
        }
        catch (Exception ex)
        {
            return ErrorResponse.HandleInternalServerError(ex.Message);
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

            return SuccessResponse.HandleOk("Successfully login", response, token);
        }
        catch (ForbiddenException ex)
        {
            return ErrorResponse.HandleForbidden(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return ErrorResponse.HandleNotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ErrorResponse.HandleInternalServerError(ex.Message);
        }
    }

}