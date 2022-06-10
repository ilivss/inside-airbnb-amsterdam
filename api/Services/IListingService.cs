using api.Models;

namespace api.Services;

public interface IListingService : IGenericCrudService<Listing>
{
    Task<IEnumerable<ListingDTO>> Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews);
    Task<ListingDetailsDTO?> Get(int id);
}
