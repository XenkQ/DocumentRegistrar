using Backend.Services;
using Dtos.DocumentPositionDtos;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/document-position")]
[ApiController]
public class DocumentPositionController : ControllerBase
{
    private readonly IDocumentPositionService _documentPositionService;

    public DocumentPositionController(IDocumentPositionService documentPositionService)
    {
        _documentPositionService = documentPositionService;
    }

    [HttpGet("under-admission-document/{admissionDocumentId}")]
    public ActionResult<IEnumerable<string>> GetPositionDocumentsUnderAdmissionDocument(int admissionDocumentId)
    {
        return Ok(_documentPositionService.GetDocumentPositionsUnderAdmissionDocument(admissionDocumentId));
    }

    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        DocumentPositionDto? documentPosition = _documentPositionService.GetById(id);

        if (documentPosition is null)
        {
            return NotFound();
        }

        return Ok(documentPosition);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateDocumentPositionDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = _documentPositionService.Create(dto);

        return Created($"api/document-position/{id}", id);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateDocumentPositionDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool isUpdated = _documentPositionService.Update(id, dto);
        if (isUpdated)
        {
            return Ok();
        }

        return NotFound();
    }
}
