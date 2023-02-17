using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;
using AuthenticationService.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IAuthenticationService<UserLogin> _userService;

    public LoginController(IConfiguration configuration,
        IAuthenticationService<UserLogin> userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [HttpPost]
    [Authorize]
    [Route("RegisterUser")]
    public async Task<IActionResult> RegisterUser([FromBody] UserLogin userLogin)
    {
        if(userLogin == null)
        {
            return BadRequest();
        }

        userLogin.Id = Guid.NewGuid().ToString();
        await _userService.AddData(userLogin);
        return Ok(userLogin);
    }

    [HttpPost]
    public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return View(loginDto);
        }

        if(!await _userService.ValidateData(loginDto.UserID))
        {
            return BadRequest($"UserId not found : {loginDto.UserID}");
        }

        if(! await ValidatePassword(loginDto))
        {
            return BadRequest($"Password does not match");
        }

        var loginUser = await _userService.GetDataById(loginDto.UserID);
        if (loginUser != null)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in loginUser.Roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                user = loginUser.UserName
            });
        }

        return Unauthorized();
    }

    private async Task<bool> ValidatePassword(LoginDto userDto)
    {
        var user = await _userService.GetDataById(userDto.UserID);
        if(user.Password == userDto.Password)
        {
            return true;
        }
        return false;
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
