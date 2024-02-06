using System.Net;

namespace SocialMediaApp.Exceptions;
public class InternalServerException : ApplicationException
{
    public InternalServerException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}