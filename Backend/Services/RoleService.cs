using AutoMapper;
using Backend.Data;
using Backend.Entities;
using Dtos.RoleDto;

namespace Backend.Services;

public interface IRoleService
{
    public IEnumerable<RoleDto> GetAll();

    public RoleDto GetById(int id);
}

public class RoleService : IRoleService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public RoleService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public RoleDto GetById(int id)
    {
        Role? Role = _dbContext
            .Roles
            .FirstOrDefault(r => r.Id == id);

        if (Role is null)
        {
            return null;
        }

        return _mapper.Map<RoleDto>(Role);
    }

    public IEnumerable<RoleDto> GetAll()
    {
        List<Role> Roles = _dbContext
            .Roles
            .ToList();

        return _mapper.Map<List<RoleDto>>(Roles);
    }
}
