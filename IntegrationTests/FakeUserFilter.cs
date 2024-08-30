using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IntegrationTests;

public class FakeUserFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, "Test User"),
            new Claim(ClaimTypes.Email, "example@gmail.com")
        }, "FakeScheme"));
        context.HttpContext.User = claimsPrincipal;
        await next();
    }
}