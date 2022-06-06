using client.Models;
using System.Net.Http.Json;

namespace client.Services;

public class NeighbourhoodService : INeighbourhoodService
{
    private HttpClient _httpClient;

    public NeighbourhoodService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Neighbourhood>?> Get()
    {
        return await _httpClient.GetFromJsonAsync<List<Neighbourhood>>("/neighbourhood");
    }
}
