using client.Models;
using System.Net.Http.Json;

namespace client.Services;

public class StatisticsService : IStatisticsService
{
    private HttpClient _httpClient;

    public StatisticsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<NeighbourhoodAverageAvailability>?> GetAverageAvailabilityPerNeighbourhood()
    {
        return await _httpClient.GetFromJsonAsync<List<NeighbourhoodAverageAvailability>>("/statistics/averageavailabilityperneighbourhood");
    }

    public async Task<List<NeighbourhoodAverageBeds>?> GetAverageBedsPerNeighbourhood()
    {
        return await _httpClient.GetFromJsonAsync<List<NeighbourhoodAverageBeds>>("/statistics/averagebedsperneighbourhood");
    }

    public async Task<List<NeighbourhoodAveragePrice>?> GetAveragePricePerNeighbourhood()
    {
        return await _httpClient.GetFromJsonAsync<List<NeighbourhoodAveragePrice>>("/statistics/averagepriceperneighbourhood");
    }

    public async Task<List<NeighbourhoodListingCount>?> GetListingCountPerNeighbourhood()
    {
        return await _httpClient.GetFromJsonAsync<List<NeighbourhoodListingCount>>("/statistics/listingcountperneighbourhood");
    }
}
