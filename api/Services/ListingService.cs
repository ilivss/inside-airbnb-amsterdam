using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class ListingService : IListingService
{

    private readonly ApplicationContext _context;
    public ListingService(ApplicationContext context)
    {
        _context = context;
    }

    // public async Task<IEnumerable<Listing>> Get()
    // {
    //     return await _context.Listings.ToListAsync();
    // }

    public async Task<IEnumerable<ListingDTO>> Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    {
        return await _context.Listings
            .AsNoTracking()
            .Where(l =>
                (minPrice == null || l.Price > minPrice) &&
                (maxPrice == null || l.Price < maxPrice) &&
                (neighbourhood == null || l.Neighbourhood.ToLower().Equals(neighbourhood.ToLower())) &&
                (minNrOfReviews == null || l.NumberOfReviews > minNrOfReviews) &&
                (maxNrOfReviews == null || l.NumberOfReviews < maxNrOfReviews)
            )
            .Select(l => new ListingDTO
            {
                Id = l.Id,
                Name = l.Name,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                RoomType = l.RoomType,
            })
            .ToListAsync();
    }

    public async Task<Listing?> Get(int id)
    {
        return await _context.Set<Listing>().FindAsync(id);
    }

    public async Task<Listing> Create(Listing listing)
    {
        _context.Set<Listing>().Add(listing);
        await _context.SaveChangesAsync();
        return listing;
    }

    public async Task<Listing> Update(Listing listing)
    {
        _context.Entry(listing).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return listing;
    }

    public async Task Delete(int id)
    {
        var listing = _context.Set<Listing>().Find(id);
        if (listing != null)
        {
            _context.Set<Listing>().Remove(listing);
            await _context.SaveChangesAsync();
        }

        return;
    }
}
