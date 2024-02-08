using System.Text.Json.Serialization;

namespace SocialMediaApp.Responses;

public class ErrorResponseModel
{
    public required string Error { get; set; }
    public  int StatusCode { get; set; }
    // public  string? Title { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StackTrace { get; set; }

    public required string Message { get; set; }
    // public  string? ExceptionMessage { get; set; }

}