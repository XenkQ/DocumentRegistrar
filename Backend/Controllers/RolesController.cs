using Backend.Services;
using Dtos.RoleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("/api/role")]
[Controller]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RoleDto>> Get()
    {
        return Ok(_roleService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<RoleDto> Get(int id)
    {
        RoleDto? role = _roleService.GetById(id);

        if (role is null)
        {
            return NotFound();
        }

        return Ok(role);
    }
}
