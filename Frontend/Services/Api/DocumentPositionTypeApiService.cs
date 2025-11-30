using Dtos.AdmissionDocumentDtos;
using Dtos.CreateDocumentTypeDtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services.Api;

public interface IDocumentPositionTypeApiService
{
    public Task<IEnumerable<DocumentPositionTypeDto>> GetDocumentPositionTypesAsync();

    public Task<DocumentPositionTypeDto> GetDocumentPositionTypeAsync(int id);

    public Task<int> CreateDocumentPositionTypeAsync(CreateDocumentPositionTypeDto dto);

    public Task UpdateDocumentPositionTypeAsync(int id, UpdateDocumentPositionTypeDto dto);
}

public class DocumentPositionTypeApiService : IDocumentPositionTypeApiService
{
    private readonly HttpClient _httpClient;

    public DocumentPositionTypeApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BackendApi");
    }

    public async Task<IEnumerable<DocumentPositionTypeDto>> GetDocumentPositionTypesAsync()
    {
        var response = await _httpClient.GetAsync($"api/document-position-type");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<DocumentPositionTypeDto>>();
    }

    public async Task<DocumentPositionTypeDto> GetDocumentPositionTypeAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/document-position-type/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<DocumentPositionTypeDto>();
    }

    public async Task<int> CreateDocumentPositionTypeAsync(CreateDocumentPositionTypeDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/document-position-type", dto);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task UpdateDocumentPositionTypeAsync(int id, UpdateDocumentPositionTypeDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/document-position-type/{id}", dto);

        response.EnsureSuccessStatusCode();
    }
}
