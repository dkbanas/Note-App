using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces;
public interface ITokenService
{
    string CreateToken(IdentityUser user);
}