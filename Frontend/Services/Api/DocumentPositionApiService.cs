using Dtos.DocumentPositionDtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services.Api;

public interface IDocumentPositionApiService
{
    Task<DocumentPositionDto> GetDocumentPositionAsync(int id);

    Task<IEnumerable<DocumentPositionDto>> GetDocumentPositionsAsync();

    Task<int> CreateDocumentPositionAsync(CreateDocumentPositionDto dto);

    Task UpdateDocumentPositionAsync(int id, UpdateDocumentPositionDto dto);
}

public class DocumentPositionApiService : IDocumentPositionApiService
{
    private readonly HttpClient _httpClient;

    public DocumentPositionApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<DocumentPositionDto>> GetDocumentPositionsAsync()
    {
        var response = await _httpClient.GetAsync("api/document-position");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<DocumentPositionDto>>();
    }

    public async Task<DocumentPositionDto> GetDocumentPositionAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/document-position/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<DocumentPositionDto>();
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
