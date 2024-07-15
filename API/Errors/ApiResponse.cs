namespace API.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }

    private string? GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            400 =>
                "Bad Request.The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).",
            401 => "Unauthorized.The client must authenticate itself to get the requested response.",
            404 =>
                "Not Found.The server cannot find the requested resource.In an API, this can also mean that the endpoint is valid but the resource itself does not exist.",
            500 => "Internal Server Error. The server has encountered a situation it does not know how to handle.",
            _ => null
        };
    }
}