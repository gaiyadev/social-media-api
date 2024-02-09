using System.Net;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Database;
using SocialMediaApp.DTOs;
using SocialMediaApp.Exceptions;
using SocialMediaApp.Pagination;
using SocialMediaApp.Services.Services;


namespace SocialMediaApp.Services.Post;

public class PostService : IPostService
{
    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<PostService> _logger;

    public PostService(DatabaseContext databaseContext, ILogger<PostService> logger)
    {
        _databaseContext = databaseContext;
        _logger = logger;
    }

    public async Task<Entities.Post> CreatePost(CreatePostDto createPostDto, int userId)
    {
        try
        {


            var post = new Entities.Post()
            {
                Content = createPostDto.Content,
                UserId = userId,
            };

            await _databaseContext.AddAsync(post);
            await _databaseContext.SaveChangesAsync();
            return post;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<PagedResult<Entities.Post>> FetchPosts(int page, int itemsPerPage, string search)
    {
        try
        {
            // Base query without search term
            var query = _databaseContext.Posts
                .Include(post => post.User)
                // .Include(vendor => vendor.User)
                // .ThenInclude(role => role.Role)
                .OrderByDescending(post => post.Id);

            // Apply search filter if a search term is provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = (IOrderedQueryable<Entities.Post>)query.Where(post => post.Content.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var products = await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            // Create the pagination metadata
            var meta = new PaginationMetaData
            {
                TotalItems = totalItems,
                ItemCount = products.Count,
                ItemsPerPage = itemsPerPage,
                TotalPages = totalPages,
                CurrentPage = page
            };
            var paginationLinks = new PaginationLinks("http://localhost:5178/api/", page, totalPages, itemsPerPage);

            // Create the paged result
            return new PagedResult<Entities.Post>
            {
                Data = products,
                Meta = meta,
                Links = paginationLinks
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}