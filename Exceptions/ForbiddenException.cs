using System.Net;

namespace SocialMediaApp.Exceptions;

public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}