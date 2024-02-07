using FluentValidation;

namespace SocialMediaApp.DTOs;

public class SignInDto
{
     public required string Email { get; set; }

    public required string Password { get; set; }
}

public class SignInDtoValidator : AbstractValidator<SignInDto> 
{
     public SignInDtoValidator()
    {
        RuleFor(rule => rule.Email).NotEmpty()
        .WithMessage("Email should not be empty")
        .EmailAddress();
        
      
        RuleFor(rule => rule.Password).NotEmpty()
        .WithMessage("Password should not be empty")
        .MinimumLength(6).WithMessage("Password too short");
        
    }
}