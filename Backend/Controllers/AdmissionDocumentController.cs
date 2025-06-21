using Backend.Models.AdmissionDocumentDtos;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/admission-document")]
[ApiController]
public class AdmissionDocumentController : ControllerBase
{
    private readonly IAdmissionDocumentService _admissionDocumentService;

    public AdmissionDocumentController(IAdmissionDocumentService admissionDocumentService)
    {
        _admissionDocumentService = admissionDocumentService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AdmissionDocumentDto>> Get()
    {
        return Ok(_admissionDocumentService.GetAll());
    }

    [HttpGet("{id}")]
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
    public ActionResult Create([FromBody] CreateAdmissionDocumentDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = _admissionDocumentService.Create(dto);

        return Created($"api/admission-document/{id}", id);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateAdmissionDocumentDto value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool isUpdated = _admissionDocumentService.Update(id, value);
        if (isUpdated)
        {
            return Ok();
        }

        return NotFound();
    }
}
