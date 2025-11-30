using Dtos.UserDtos;
using Frontend.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Frontend.Services.Api;

public interface IAccountApiService
{
    Task LoginAsync(LoginUserDto dto);

    Task RegisterAsync(RegisterUserDto dto);
}

public class AccountApiService : IAccountApiService
{
    private readonly IUserContextService _userService;
    private readonly HttpClient _httpClient;

    public AccountApiService(IHttpClientFactory httpClientFactory, IUserContextService userService)
    {
        _httpClient = httpClientFactory.CreateClient("BackendApi");
        _userService = userService;
    }

    public async Task LoginAsync(LoginUserDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/account/login", dto);

        string token = await response.Content.ReadAsStringAsync();

        var handler = new JwtSecurityTokenHandler();

        if (handler.CanReadToken(token))
        {
            JwtSecurityToken jsonToken = handler.ReadJwtToken(token);
            var id = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var email = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var roleName = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            await _userService.SetAndSaveUserAsync(new User()
            {
                Id = int.Parse(id),
                Name = name,
                Email = email,
                RoleName = roleName,
                BearerToken = token
            });
        }
    }

    public async Task RegisterAsync(RegisterUserDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/account/register", dto);

        response.EnsureSuccessStatusCode();
    }
}
