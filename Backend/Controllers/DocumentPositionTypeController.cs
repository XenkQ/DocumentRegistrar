using Backend.Helpers;
using Backend.Services;
using Dtos.CreateDocumentTypeDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/document-position-type")]
[ApiController]
[Authorize(Roles = "Admin,Manager")]
public class DocumentPositionTypeController : ControllerBase
{
    private readonly IDocumentPositionTypeService _documentPositionTypeService;

    public DocumentPositionTypeController(IDocumentPositionTypeService documentPositionTypeService)
    {
        _documentPositionTypeService = documentPositionTypeService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager,User")]
    public ActionResult<IEnumerable<string>> GetAll()
    {
        return Ok(_documentPositionTypeService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        DocumentPositionTypeDto? documentPosition = _documentPositionTypeService.GetById(id);

        if (documentPosition is null)
        {
            return NotFound();
        }

        return Ok(documentPosition);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateDocumentPositionTypeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return ControllerHelper.HandleCreate(
            this,
            () => _documentPositionTypeService.Create(dto),
            "api/document-position-type"
        );
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateDocumentPositionTypeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return ControllerHelper.HandleUpdate(
            this,
            () => _documentPositionTypeService.Update(id, dto)
        );
    }
}
