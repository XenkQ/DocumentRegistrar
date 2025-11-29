using Backend.Helpers;
using Backend.Services;
using Dtos.ContractorsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/contractor")]
[ApiController]
[Authorize(Roles = "Admin,Manager")]
public class ContractorController : ControllerBase
{
    private readonly IContractorService _contractorService;

    public ContractorController(IContractorService contractorService)
    {
        _contractorService = contractorService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager,User")]
    public ActionResult<IEnumerable<string>> Get()
    {
        return Ok(_contractorService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager,User")]
    public ActionResult Get(int id)
    {
        ContractorDto? contractor = _contractorService.GetById(id);

        if (contractor is null)
        {
            return NotFound();
        }

        return Ok(contractor);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateContractorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return ControllerHelper.HandleCreate(
            this,
            () => _contractorService.Create(dto),
            "api/contractor"
        );
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateContractorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return ControllerHelper.HandleUpdate(
            this,
            () => _contractorService.Update(id, dto)
        );
    }
}
