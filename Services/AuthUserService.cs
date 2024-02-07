using System.Security.Claims;

namespace SocialMediaApp.Services.Services;

public sealed class AuthUserService
{
    public int GetUserId(ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst("id");

        if (idClaim != null && int.TryParse(idClaim.Value, out int userId))
        {
            return userId;
        }
        throw new InvalidOperationException("User ID not found in claims.");
    }
}