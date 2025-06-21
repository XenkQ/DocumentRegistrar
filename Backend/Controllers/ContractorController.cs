using Backend.Models.ContractorsDtos;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/contractor")]
[ApiController]
public class ContractorController : ControllerBase
{
    private readonly IContractorService _contractorService;

    public ContractorController(IContractorService contractorService)
    {
        _contractorService = contractorService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return Ok(_contractorService.GetAll());
    }

    [HttpGet("{id}")]
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

        var id = _contractorService.Create(dto);

        return Created($"api/contractor/{id}", id);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateContractorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool isUpdated = _contractorService.Update(id, dto);
        if (isUpdated)
        {
            return Ok();
        }

        return NotFound();
    }
}
