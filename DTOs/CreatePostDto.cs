using FluentValidation;

namespace SocialMediaApp.DTOs;

public class CreatePostDto
{
    public required string Content { get; set; }

}

public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
{
    public CreatePostDtoValidator()
    {
        RuleFor(rule => rule.Content)
           .NotEmpty()
           .WithMessage("Content should not be empty.")
           .Matches(@"^[a-zA-Z\s]+$")
           .WithMessage("Content must only contain letters, and spaces.");
    }
}