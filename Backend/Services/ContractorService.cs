using AutoMapper;
using Backend.Data;
using Backend.Entities;
using Backend.Models.ContractorsDtos;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public interface IContractorService
{
    public int Create(CreateContractorDto dto);

    public IEnumerable<ContractorDto> GetAll();

    public ContractorDto GetById(int id);

    public bool Update(int id, UpdateContractorDto dto);
}

public class ContractorService : IContractorService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ContractorService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public ContractorDto GetById(int id)
    {
        Contractor? Contractor = _dbContext
            .Contractors
            .Include(c => c.AdmissionDocuments)
            .FirstOrDefault(ad => ad.Id == id);

        if (Contractor is null)
        {
            return null;
        }

        return _mapper.Map<ContractorDto>(Contractor);
    }

    public IEnumerable<ContractorDto> GetAll()
    {
        List<Contractor> Contractors = _dbContext
            .Contractors
            .Include(c => c.AdmissionDocuments)
            .ToList();

        return _mapper.Map<List<ContractorDto>>(Contractors);
    }

    public int Create(CreateContractorDto dto)
    {
        Contractor Contractor = _mapper.Map<Contractor>(dto);

        _dbContext.Add(Contractor);

        _dbContext.SaveChanges();

        return Contractor.Id;
    }

    public bool Update(int id, UpdateContractorDto dto)
    {
        Contractor? Contractor = _dbContext
            .Contractors
            .FirstOrDefault(ad => ad.Id == id);

        if (Contractor is null)
        {
            return false;
        }

        _mapper.Map(dto, Contractor);

        _dbContext.SaveChanges();

        return true;
    }
}
