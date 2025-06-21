using Dtos.ContractorsDtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services.Api;

public interface IContractorApiService
{
    public Task<ContractorDto> GetContractorAsync(int id);

    public Task<IEnumerable<ContractorDto>> GetContractorsAsync();

    public Task<int> CreateContractorAsync(CreateContractorDto dto);

    public Task UpdateContractorAsync(int id, UpdateContractorDto dto);
}

public class ContractorApiService : IContractorApiService
{
    private readonly HttpClient _httpClient;

    public ContractorApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ContractorDto>> GetContractorsAsync()
    {
        var response = await _httpClient.GetAsync("api/contractor");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<ContractorDto>>();
    }

    public async Task<ContractorDto> GetContractorAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/contractor/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ContractorDto>();
    }

    public async Task<int> CreateContractorAsync(CreateContractorDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/contractor", dto);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task UpdateContractorAsync(int id, UpdateContractorDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/contractor/{id}", dto);

        response.EnsureSuccessStatusCode();
    }
}
