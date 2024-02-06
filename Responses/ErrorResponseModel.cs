namespace BookstoreAPI.CustomResponses.Responses;

public class ErrorResponseModel
{
    public required string Error { get; set; }
    public required int StatusCode { get; set; }
    public required string Title { get; set; }
}