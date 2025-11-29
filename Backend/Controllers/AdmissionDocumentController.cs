using Backend.Helpers;
using Backend.Services;
using Dtos.AdmissionDocumentDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/admission-document")]
[ApiController]
[Authorize]
public class AdmissionDocumentController : ControllerBase
{
    private readonly IAdmissionDocumentService _admissionDocumentService;

    public AdmissionDocumentController(IAdmissionDocumentService admissionDocumentService)
    {
        _admissionDocumentService = admissionDocumentService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager,User")]
    public ActionResult<IEnumerable<AdmissionDocumentDto>> Get()
    {
        return Ok(_admissionDocumentService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager,User")]
    public ActionResult<AdmissionDocumentDto> Get(int id)
    {
        AdmissionDocumentDto admissionDocumentDto = _admissionDocumentService.GetById(id);

        if (admissionDocumentDto is null)
        {
            return NotFound();
        }

        return Ok(admissionDocumentDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager,User")]
    public ActionResult Create([FromBody] CreateAdmissionDocumentDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return ControllerHelper.HandleCreate(
            this,
            () => _admissionDocumentService.Create(dto),
            "api/admission-document"
        );
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public ActionResult Update(int id, [FromBody] UpdateAdmissionDocumentDto value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return ControllerHelper.HandleUpdate(
            this,
            () => _admissionDocumentService.Update(id, value)
        );
    }
}
