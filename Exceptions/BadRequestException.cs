using System.Net;

namespace SocialMediaApp.Exceptions;

public class BadRequestException : ApplicationException
{
     public BadRequestException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
    
}