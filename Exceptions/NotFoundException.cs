using System.Net;

namespace SocialMediaApp.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}