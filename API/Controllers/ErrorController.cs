using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("errors/{code}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}