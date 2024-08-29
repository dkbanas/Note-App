using System.Security.Claims;
using API.Errors;
using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var user = await _userManager.FindByEmailAsync(email);
        return new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            Username = user.UserName,
        };
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
        {
            return BadRequest(new ApiResponse(400, "Invalid login request"));
        }
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if(user == null) return Unauthorized(new ApiResponse(401));
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        if(!result.Succeeded) return Unauthorized(new ApiResponse(401));

        return new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            Username = user.UserName,
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (CheckEmailExists(registerDto.Email).Result.Value)
        {
            return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new []{"Email address is in use"}});
        }

        
        var user = new IdentityUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if(!result.Succeeded) return BadRequest(new ApiResponse(400));
        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email,
        };
    }
    
    [Authorize]
    [HttpPut("updateUsername")]
    public async Task<ActionResult> UpdateUser( UpdateUserDto updateUserDto)
    {
        var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return NotFound();

        user.UserName = updateUserDto.Username ?? user.UserName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return NoContent();
    }

    [Authorize]
    [HttpPut("updatePassword")]
    public async Task<ActionResult> UpdatePassword(UpdatePasswordDto updatePasswordDto)
    {
        var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return NotFound();

        var result = await _userManager.ChangePasswordAsync(user, updatePasswordDto.CurrentPassword, updatePasswordDto.NewPassword);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return NoContent();
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteAccount()
    {
        var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return NoContent();
    }
}