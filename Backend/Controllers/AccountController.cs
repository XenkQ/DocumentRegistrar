using Backend.Services;
using Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("/api/account")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService registerService)
    {
        _accountService = registerService;
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
    {
        _accountService.RegisterUser(dto);

        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult LoginUser([FromBody] LoginUserDto dto)
    {
        var token = _accountService.GenerateJwt(dto);

        return Ok(token);
    }
}
