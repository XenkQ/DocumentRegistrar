using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dtos.UserDtos;

namespace Frontend.Services.Api;

public interface IUserApiService
{
    Task<IEnumerable<UserDto>> GetUsersAsync();

    Task<UserDto> GetUserAsync(int id);

    Task UpdateUserAsync(int id, UpdateUserDto dto);
}

public class UserApiService : IUserApiService
{
    private readonly HttpClient _httpClient;

    public UserApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BackendApi");
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var response = await _httpClient.GetAsync("api/user");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();
    }

    public async Task<UserDto> GetUserAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/user/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/user/{id}", dto);

        response.EnsureSuccessStatusCode();
    }
}
