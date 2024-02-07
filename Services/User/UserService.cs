using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Database;
using SocialMediaApp.DTOs;
using SocialMediaApp.Exceptions;
using System.Net;

namespace SocialMediaApp.Services.User;


public class UserService : IUserService
{
    private readonly DatabaseContext _databaseContext;
    private readonly PasswordService _passwordService;
    private readonly ILogger<UserService> _logger;

    public UserService(DatabaseContext databaseContext, PasswordService passwordService, ILogger<UserService> logger)
    {
        _databaseContext = databaseContext;
        _passwordService = passwordService;
        _logger = logger;
    }

    private async Task<Entities.User?> GetUserByEmail(string email)
    {
        return await _databaseContext.Users.Where(user => user.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task<Entities.User> SignIn(SignInDto signInDto)
    {
        var user = await GetUserByEmail(signInDto.Email);

        if (user == null)
        {
            throw new ForbiddenException("Invalid email or password.", HttpStatusCode.Forbidden);
        }

        if (!_passwordService.VerifyPassword(signInDto.Password, user.Password))
        {
            throw new ForbiddenException("Invalid email or password.", HttpStatusCode.Forbidden);
        }

        return user;
    }


    public async Task<Entities.User> Signup(SignUpDto signUpDto)
    {
        var findEmail = await _databaseContext.Users.AnyAsync(user => user.Email == signUpDto.Email);

        if (findEmail)
        {
            throw new ConflictException("Email address already taken", HttpStatusCode.Conflict);
        }

        try
        {
            string hashedPassword = _passwordService.HashPassword(signUpDto.Password);

            var user = new Entities.User()
            {
                Email = signUpDto.Email,
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                Password = hashedPassword,
            };

            await _databaseContext.Users.AddAsync(user);
            await _databaseContext.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

}