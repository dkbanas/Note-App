using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<IdentityUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new IdentityUser
            {
                UserName = "Test",
                Email = "test@test.com",
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}