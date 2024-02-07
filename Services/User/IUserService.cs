using SocialMediaApp.DTOs;

namespace SocialMediaApp.Services.User;

public interface IUserService
{
    Task<Entities.User> Signup(SignUpDto signUpDto);
    
    Task<Entities.User> SignIn(SignInDto signInDto);
    
    // Task<Models.User> GetUserByEmail(string email);
    
    
    // Task<Models.User> GetUserById(int id);
    
    // Task<List<Models.User>> FindAll();
    
    // Task<Models.User> DeleteUserById(int id);

    
}