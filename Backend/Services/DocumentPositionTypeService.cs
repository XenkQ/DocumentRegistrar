using AutoMapper;
using Backend.Data;
using Backend.Entities;
using Dtos.CreateDocumentTypeDtos;

namespace Backend.Services;

public interface IDocumentPositionTypeService
{
    public int Create(CreateDocumentPositionTypeDto dto);

    public DocumentPositionTypeDto GetById(int id);

    public IEnumerable<DocumentPositionTypeDto> GetAll();

    public bool Update(int id, UpdateDocumentPositionTypeDto dto);
}

public class DocumentPositionTypeService : IDocumentPositionTypeService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public DocumentPositionTypeService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<DocumentPositionTypeDto> GetAll()
    {
        List<DocumentPositionType> documentPositionTypes = _dbContext
            .DocumentPositionTypes
            .ToList();

        return _mapper.Map<List<DocumentPositionTypeDto>>(documentPositionTypes);
    }

    public DocumentPositionTypeDto GetById(int id)
    {
        DocumentPositionType? documentPositionType = _dbContext
            .DocumentPositionTypes
            .FirstOrDefault(dpt => dpt.Id == id);

        if (documentPositionType is null)
        {
            return null;
        }

        return _mapper.Map<DocumentPositionTypeDto>(documentPositionType);
    }

    public int Create(CreateDocumentPositionTypeDto dto)
    {
        DocumentPositionType DocumentPositionType = _mapper.Map<DocumentPositionType>(dto);

        _dbContext.Add(DocumentPositionType);

        _dbContext.SaveChanges();

        return DocumentPositionType.Id;
    }

    public bool Update(int id, UpdateDocumentPositionTypeDto dto)
    {
        DocumentPositionType? documentPositionType = _dbContext
            .DocumentPositionTypes
            .FirstOrDefault(dpt => dpt.Id == id);

        if (documentPositionType is null)
        {
            return false;
        }

        _mapper.Map(dto, documentPositionType);

        _dbContext.SaveChanges();

        return true;
    }
}
