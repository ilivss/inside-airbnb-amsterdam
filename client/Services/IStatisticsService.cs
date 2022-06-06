using client.Models;

namespace client.Services;

public interface IStatisticsService
{
    Task<List<NeighbourhoodAverageAvailability>?> GetAverageAvailabilityPerNeighbourhood();
    Task<List<NeighbourhoodAverageBeds>?> GetAverageBedsPerNeighbourhood();
    Task<List<NeighbourhoodAveragePrice>?> GetAveragePricePerNeighbourhood();
    Task<List<NeighbourhoodListingCount>?> GetListingCountPerNeighbourhood();
}
