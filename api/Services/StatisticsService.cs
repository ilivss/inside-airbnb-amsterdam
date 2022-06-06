using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class StatisticsService : IStatisticsService
{
    private ApplicationContext _context;

    public StatisticsService(ApplicationContext context)
    {
        _context = context;
    }

    public IEnumerable<NeighbourhoodListingCount> GetListingCountPerNeighbourhood()
    {
        return _context.Listings.GroupBy(l => l.Neighbourhood)
                                .Select(l => new NeighbourhoodListingCount { Neighbourhood = l.Key, Count = l.Count() })
                                .ToList();
    }

    public IEnumerable<NeighbourhoodAveragePrice> GetAveragePricePerNeighbourhood()
    {
        return _context.Listings.GroupBy(l => l.Neighbourhood)
                                .Select(n => new NeighbourhoodAveragePrice { Neighbourhood = n.Key, AveragePrice = n.Average(l => l.Price) })
                                .ToList();
    }

    public IEnumerable<NeighbourhoodAverageAvailability> GetAverageAvailabilityPerNeighbourhood()
    {
        return _context.Listings.GroupBy(l => l.Neighbourhood)
                                .Select(n => new NeighbourhoodAverageAvailability { Neighbourhood = n.Key, AverageAvailability = n.Average(l => l.Availability365) })
                                .ToList();
    }

    public IEnumerable<NeighbourhoodAverageBeds> GetAverageBedsPerNeighbourhood()
    {
        return _context.Listings.GroupBy(l => l.Neighbourhood)
                                .Select(n => new NeighbourhoodAverageBeds { Neighbourhood = n.Key, AverageBeds = n.Average(l => l.Beds) })
                                .ToList();
    }
}
