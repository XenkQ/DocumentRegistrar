using AutoMapper;
using Backend.Data;
using Backend.Entities;
using Dtos.AdmissionDocumentDtos;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public interface IAdmissionDocumentService
{
    public int Create(CreateAdmissionDocumentDto dto);

    public IEnumerable<AdmissionDocumentDto> GetAll();

    public AdmissionDocumentDto GetById(int id);

    public bool Update(int id, UpdateAdmissionDocumentDto dto);
}

public class AdmissionDocumentService : IAdmissionDocumentService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public AdmissionDocumentService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public AdmissionDocumentDto GetById(int id)
    {
        AdmissionDocument? admissionDocument = _dbContext
            .AdmissionDocuments
            .Include(ad => ad.Contractor)
            .Include(ad => ad.DocumentPositions)
            .FirstOrDefault(ad => ad.Id == id);

        if (admissionDocument is null)
        {
            return null;
        }

        return _mapper.Map<AdmissionDocumentDto>(admissionDocument);
    }

    public IEnumerable<AdmissionDocumentDto> GetAll()
    {
        List<AdmissionDocument> admissionDocuments = _dbContext
            .AdmissionDocuments
            .Include(ad => ad.Contractor)
            .Include(ad => ad.DocumentPositions)
            .ToList();

        return _mapper.Map<List<AdmissionDocumentDto>>(admissionDocuments);
    }

    public int Create(CreateAdmissionDocumentDto dto)
    {
        AdmissionDocument admissionDocument = _mapper.Map<AdmissionDocument>(dto);

        _dbContext.Add(admissionDocument);

        _dbContext.SaveChanges();

        return admissionDocument.Id;
    }

    public bool Update(int id, UpdateAdmissionDocumentDto dto)
    {
        AdmissionDocument? admissionDocument = _dbContext
            .AdmissionDocuments
            .FirstOrDefault(ad => ad.Id == id);

        if (admissionDocument is null)
        {
            return false;
        }

        Contractor? contractor = _dbContext
            .Contractors
            .FirstOrDefault(c => c.Id == dto.ContractorId);

        if (contractor is null)
        {
            return false;
        }

        _mapper.Map(dto, admissionDocument);

        _dbContext.SaveChanges();

        return true;
    }
}
