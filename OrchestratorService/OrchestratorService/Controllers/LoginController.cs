using Microsoft.AspNetCore.Mvc;
using OrchestratorService.Models;

namespace OrchestratorService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : Controller
{
    List<UserLogin> user = new List<UserLogin>();
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("RegisterUser")]
    public async Task<IActionResult> RegisterUser([FromBody] UserLogin userLogin)
    {
        if(userLogin == null)
        {
            return BadRequest();
        }

        userLogin.Id = Guid.NewGuid().ToString();
        user.Add(userLogin);
        return Ok(userLogin);
    }

    [HttpPost]
    public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return View(loginDto);
        }

        if(!await ValidateUser(loginDto.UserID))
        {
            return BadRequest($"UserId not found : {loginDto.UserID}");
        }

        if(! await ValidatePassword(loginDto))
        {
            return BadRequest($"Password does not match");
        }

        var loginUser = user.FirstOrDefault(x => x.UserName == loginDto.UserID);

        return Ok("Test Token");
    }

    private async Task<bool> ValidateUser(string userId)
    {
        if(user.Any(x=> x.UserName ==  userId))
        {
            return true;
        }
        return false;
    }

    private async Task<bool> ValidatePassword(LoginDto userDto)
    {
        if(user.Any(x=> x.UserName == userDto.UserID && x.Password == userDto.Password))
        {
            return true;
        }
        return false;
    }
}
