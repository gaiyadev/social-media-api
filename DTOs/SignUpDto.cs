using FluentValidation;

namespace SocialMediaApp.DTOs;

public class SignUpDto
{
    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Password { get; set; }

    public required string ConfirmPassword { get; set; }
}

public class SignUpDtoValidator : AbstractValidator<SignUpDto> 
{
     public SignUpDtoValidator()
    {
        RuleFor(rule => rule.Email).NotEmpty()
        .WithMessage("Email should not be empty")
        .EmailAddress();
        
        RuleFor(rule => rule.FirstName).NotEmpty()
        .WithMessage("First name should not be empty")
        .Matches(@"^[a-zA-Z]+$")
            .WithMessage("First name must only contain letters.");
        
        RuleFor(rule => rule.LastName).NotEmpty()
        .WithMessage("Last name should not be empty")
        .Matches(@"^[a-zA-Z]+$")
        .WithMessage("Last name must only contain letters");        
        
        RuleFor(rule => rule.Password).NotEmpty()
        .WithMessage("Password should not be empty")
        .MinimumLength(6).WithMessage("Password too short");
        
        RuleFor(rule => rule.ConfirmPassword).NotEmpty()
        .WithMessage("Confirm password should not be empty")
        .Equal(p => p.Password).WithMessage("Password mismatch");
    }
}