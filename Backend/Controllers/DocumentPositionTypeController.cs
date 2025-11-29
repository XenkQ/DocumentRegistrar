using Backend.Helpers;
using Backend.Services;
using Dtos.CreateDocumentTypeDtos;
using Dtos.DocumentPositionDtos;
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
    public ActionResult Create([FromBody] DocumentPositionTypeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return ControllerHelper.HandleCreate(
            this,
            () => _documentPositionTypeService.Create(dto),
            "api/document-position"
        );
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] DocumentPositionTypeDto dto)
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
