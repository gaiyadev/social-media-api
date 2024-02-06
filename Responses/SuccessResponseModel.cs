using System.Text.Json.Serialization;

namespace SocialMediaApp.Responses;

public class SuccessResponseModel<T>
{
    public required string Message { get; set; }
    public int StatusCode { get; set; }
    public required string Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }
    
    public required T Data { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? accessToken { get; set; }

}