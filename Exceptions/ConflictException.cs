using System.Net;

namespace SocialMediaApp.Exceptions;

public class ConflictException : ApplicationException
{
    public ConflictException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}