using Backend.Data;
using Backend.Entities;
using Backend.Exceptions;
using Backend.Models;
using Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services;

public interface IAccountService
{
    void RegisterUser(RegisterUserDto dto);

    string GenerateJwt(LoginUserDto dto);
}

public class AccountService : IAccountService
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;

    public AccountService(
        AppDbContext dbContext,
        IPasswordHasher<User> passwordHasher,
        AuthenticationSettings authenticationSettings)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
    }

    public void RegisterUser(RegisterUserDto dto)
    {
        User user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            RoleId = dto.RoleId,
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public string GenerateJwt(LoginUserDto dto)
    {
        var user = _dbContext
            .Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == dto.Email);

        if (user is null)
        {
            throw new BadRequestException("Invalid email or password.");
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Invalid email or password.");
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(
            _authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred
        );

        var jwtSercurityTokenHandler = new JwtSecurityTokenHandler();
        return jwtSercurityTokenHandler.WriteToken(token);
    }
}
