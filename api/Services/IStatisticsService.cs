using api.Models;

public interface IStatisticsService
{
    IEnumerable<NeighbourhoodAverageAvailability> GetAverageAvailabilityPerNeighbourhood();
    IEnumerable<NeighbourhoodAverageBeds> GetAverageBedsPerNeighbourhood();
    IEnumerable<NeighbourhoodAveragePrice> GetAveragePricePerNeighbourhood();
    IEnumerable<NeighbourhoodListingCount> GetListingCountPerNeighbourhood();
}
