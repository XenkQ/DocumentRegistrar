using Backend.Helpers;
using Backend.Services;
using Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/user")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> Get()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        UserDto? user = _userService.GetById(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return ControllerHelper.HandleUpdate(
            this,
            () => _userService.Update(id, dto)
        );
    }
}
