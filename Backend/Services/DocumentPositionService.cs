using AutoMapper;
using Backend.Data;
using Backend.Entities;
using Dtos.DocumentPositionDtos;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public interface IDocumentPositionService
{
    public int Create(CreateDocumentPositionDto dto);

    public IEnumerable<DocumentPositionDto> GetDocumentPositionsUnderAdmissionDocument(int admissionDocumentId);

    public DocumentPositionDto GetById(int id);

    public bool Update(int id, UpdateDocumentPositionDto dto);
}

public class DocumentPositionService : IDocumentPositionService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public DocumentPositionService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public DocumentPositionDto GetById(int id)
    {
        DocumentPosition? DocumentPosition = _dbContext
            .DocumentPositions
            .Include(dp => dp.DocumentPositionType)
            .FirstOrDefault(dp => dp.Id == id);

        if (DocumentPosition is null)
        {
            return null;
        }

        return _mapper.Map<DocumentPositionDto>(DocumentPosition);
    }

    public IEnumerable<DocumentPositionDto> GetDocumentPositionsUnderAdmissionDocument(int admissionDocumentId)
    {
        AdmissionDocument? admissionDocument = _dbContext
            .AdmissionDocuments
            .Include(ad => ad.DocumentPositions)
                .ThenInclude(dp => dp.DocumentPositionType)
            .FirstOrDefault(ad => ad.Id == admissionDocumentId);

        if (admissionDocument is null)
        {
            return new List<DocumentPositionDto>();
        }

        List<DocumentPosition> documentPositions = admissionDocument.DocumentPositions;

        return _mapper.Map<List<DocumentPositionDto>>(documentPositions);
    }

    public int Create(CreateDocumentPositionDto dto)
    {
        DocumentPosition DocumentPosition = _mapper.Map<DocumentPosition>(dto);

        _dbContext.Add(DocumentPosition);

        _dbContext.SaveChanges();

        return DocumentPosition.Id;
    }

    public bool Update(int id, UpdateDocumentPositionDto dto)
    {
        DocumentPosition? documentPosition = _dbContext
            .DocumentPositions
            .FirstOrDefault(dp => dp.Id == id);

        if (documentPosition is null)
        {
            return false;
        }

        AdmissionDocument? admissionDocument = _dbContext
            .AdmissionDocuments
            .FirstOrDefault(ad => ad.Id == dto.AdmissionDocumentId);

        if (admissionDocument is null)
        {
            return false;
        }

        _mapper.Map(dto, documentPosition);

        _dbContext.SaveChanges();

        return true;
    }
}
