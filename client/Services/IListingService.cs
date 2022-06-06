using client.Models;

namespace client.Services;

public interface IListingService
{
    Task<List<Listing>?> Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews);
    Task<Listing?> Get(string id);
    // Task<List<ListingLocationDTO>?> GetLocations(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews);
}
