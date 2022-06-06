using client.Models;
using System.Net.Http.Json;

namespace client.Services;

public class ListingService : IListingService
{
    private HttpClient _httpClient;

    public ListingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Listing>?> Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    {
        return await _httpClient.GetFromJsonAsync<List<Listing>>("/listing?minPrice=" + minPrice + "&maxPrice=" + maxPrice + "&neighbourhood=" + neighbourhood + "&minNrOfReviews=" + minNrOfReviews + "&maxNrOfReviews=" + maxNrOfReviews);
    }

    public async Task<Listing?> Get(string id)
    {
        return await _httpClient.GetFromJsonAsync<Listing>("/listing/" + id);

    }

    // public async Task<List<ListingLocationDTO>?> GetLocations(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    // {
    //     return await _httpClient.GetFromJsonAsync<List<ListingLocationDTO>>("/listing/location?minPrice=" + minPrice + "&maxPrice=" + maxPrice + "&neighbourhood=" + neighbourhood + "&minNrOfReviews=" + minNrOfReviews + "&maxNrOfReviews=" + maxNrOfReviews);
    // }
}
