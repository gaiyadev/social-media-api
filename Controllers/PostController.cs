using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.DTOs;
using SocialMediaApp.Exceptions;
using SocialMediaApp.Responses;
using SocialMediaApp.Services.Post;
using SocialMediaApp.Services.Services;
using SocialMediaApp.Utils;

namespace SocialMediaApp.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    private readonly SuccessResponse _successResponse;

    private readonly ErrorResponse _errorResponse;

    private readonly AuthUserService _authUserService;


    public PostController(IPostService postService, ErrorResponse errorResponse, SuccessResponse successResponse, AuthUserService authUserService)
    {
        _postService = postService;
        _errorResponse = errorResponse;
        _successResponse = successResponse;
        _authUserService = authUserService;
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {

        try
        {
            var user = HttpContext.User;
            var userId = _authUserService.GetUserId(user);
            
            var post = await _postService.CreatePost(createPostDto, userId);

            var response = new List<object>
            {
                new { content = post.Content }
            };
            return _successResponse.HandleSuccess("Successfully created", response, (int)HttpStatusCode.Created, null);
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

    [HttpGet]
    public async Task<IActionResult> FetchPosts([FromQuery] string search = "", int page = 1, int itemsPerPage = 10)
    {
        try
        {
            itemsPerPage = itemsPerPage > 100 ? 100 : itemsPerPage;

            var post = await _postService.FetchPosts(page, itemsPerPage, search);


            return _successResponse.HandleSuccess("Successfully fetched", post, (int)HttpStatusCode.Created, null);
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


}