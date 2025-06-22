using Dtos.AdmissionDocumentDtos;
using Dtos.ContractorsDtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services.Api;

public interface IAdmissionDocumentApiService
{
    public Task<AdmissionDocumentDto> GetAdmissionDocumentAsync(int id);

    public Task<IEnumerable<AdmissionDocumentDto>> GetAdmissionDocumentsAsync();

    public Task<IEnumerable<ContractorDto>> GetContractorsAsync();

    public Task<int> CreateAdmissionDocumentAsync(CreateAdmissionDocumentDto dto);

    public Task UpdateAdmissionDocumentAsync(int id, UpdateAdmissionDocumentDto dto);
}

public class AdmissionDocumentApiService : IAdmissionDocumentApiService
{
    private readonly HttpClient _httpClient;

    public AdmissionDocumentApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BackendApi");
    }

    public async Task<IEnumerable<AdmissionDocumentDto>> GetAdmissionDocumentsAsync()
    {
        var response = await _httpClient.GetAsync("api/admission-document");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<AdmissionDocumentDto>>();
    }

    public async Task<AdmissionDocumentDto> GetAdmissionDocumentAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/admission-document/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AdmissionDocumentDto>();
    }

    public async Task<IEnumerable<ContractorDto>> GetContractorsAsync()
    {
        var response = await _httpClient.GetAsync("api/contractor");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<ContractorDto>>();
    }

    public async Task<int> CreateAdmissionDocumentAsync(CreateAdmissionDocumentDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/admission-document", dto);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task UpdateAdmissionDocumentAsync(int id, UpdateAdmissionDocumentDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/admission-document/{id}", dto);

        response.EnsureSuccessStatusCode();
    }
}
