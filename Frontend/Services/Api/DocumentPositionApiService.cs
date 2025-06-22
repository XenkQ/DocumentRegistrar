using Dtos.AdmissionDocumentDtos;
using Dtos.DocumentPositionDtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services.Api;

public interface IDocumentPositionApiService
{
    public Task<DocumentPositionDto> GetDocumentPositionAsync(int id);

    public Task<IEnumerable<DocumentPositionDto>> GetDocumentPositionsUnderAdmisionDocumentAsync(int admissionDocumentId);

    public Task<AdmissionDocumentDto> GetAdmissionDocumentAsync(int id);

    public Task<int> CreateDocumentPositionAsync(CreateDocumentPositionDto dto);

    public Task UpdateDocumentPositionAsync(int id, UpdateDocumentPositionDto dto);
}

public class DocumentPositionApiService : IDocumentPositionApiService
{
    private readonly HttpClient _httpClient;

    public DocumentPositionApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BackendApi");
    }

    public async Task<IEnumerable<DocumentPositionDto>> GetDocumentPositionsUnderAdmisionDocumentAsync(int admissionDocumentId)
    {
        var response = await _httpClient.GetAsync(
            $"api/document-position/under-admission-document/{admissionDocumentId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<DocumentPositionDto>>();
    }

    public async Task<DocumentPositionDto> GetDocumentPositionAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/document-position/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<DocumentPositionDto>();
    }

    public async Task<AdmissionDocumentDto> GetAdmissionDocumentAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/admission-document/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AdmissionDocumentDto>();
    }

    public async Task<int> CreateDocumentPositionAsync(CreateDocumentPositionDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/document-position", dto);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task UpdateDocumentPositionAsync(int id, UpdateDocumentPositionDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/document-position/{id}", dto);

        response.EnsureSuccessStatusCode();
    }
}
