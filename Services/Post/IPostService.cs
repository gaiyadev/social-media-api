using SocialMediaApp.DTOs;
using SocialMediaApp.Pagination;

namespace SocialMediaApp.Services.Post;

public interface IPostService
{
    Task<Entities.Post> CreatePost(CreatePostDto createPostDto, int userId);

    Task<PagedResult<Entities.Post>> FetchPosts(int page, int itemsPerPage, string search);


}