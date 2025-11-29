using Dtos.UserDtos;
using Frontend.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services.Api;

public interface IAccountApiService
{
    Task<User> LoginAsync(LoginUserDto dto);

    Task RegisterAsync(RegisterUserDto dto);
}

public class AccountApiService : IAccountApiService
{
    private readonly HttpClient _httpClient;

    public AccountApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BackendApi");
    }

    public async Task<User> LoginAsync(LoginUserDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/account/login", dto);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task RegisterAsync(RegisterUserDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/account/register", dto);

        response.EnsureSuccessStatusCode();
    }
}
