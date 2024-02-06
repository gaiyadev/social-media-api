using System.Net;

namespace SocialMediaApp.Exceptions;

public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}