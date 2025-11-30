using AutoMapper;
using Backend.Data;
using Backend.Entities;
using Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public interface IUserService
{
    IEnumerable<UserDto> GetAll();

    UserDto? GetById(int id);

    bool Update(int id, UpdateUserDto dto);
}

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(AppDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public UserDto? GetById(int id)
    {
        User? user = _dbContext
            .Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Id == id);

        if (user is null)
        {
            return null;
        }

        return _mapper.Map<UserDto>(user);
    }

    public IEnumerable<UserDto> GetAll()
    {
        List<User> users = _dbContext
            .Users
            .Include(u => u.Role)
            .ToList();

        return _mapper.Map<List<UserDto>>(users);
    }

    public bool Update(int id, UpdateUserDto dto)
    {
        User? user = _dbContext
            .Users
            .FirstOrDefault(u => u.Id == id);

        if (user is null)
        {
            return false;
        }

        _mapper.Map(dto, user);

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        _dbContext.SaveChanges();

        return true;
    }
}
