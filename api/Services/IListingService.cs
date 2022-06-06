using api.Models;

namespace api.Services;

public interface IListingService : IGenericCrudService<Listing>
{
    IEnumerable<Listing> Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews);
}