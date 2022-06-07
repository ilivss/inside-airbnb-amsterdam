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

    public IEnumerable<Listing> Get()
    {
        return _context.Listings.ToList();
    }

    public IEnumerable<Listing> Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    {
        return _context.Listings
            .Where(l =>
                (minPrice == null || l.Price > minPrice) &&
                (maxPrice == null || l.Price < maxPrice) &&
                (neighbourhood == null || l.Neighbourhood.ToLower().Equals(neighbourhood.ToLower())) &&
                (minNrOfReviews == null || l.NumberOfReviews > minNrOfReviews) &&
                (maxNrOfReviews == null || l.NumberOfReviews < maxNrOfReviews)
            )
            .ToList();
    }

    public Listing? Get(int id)
    {
        return _context.Set<Listing>().Find(id);
    }

    public Listing Create(Listing listing)
    {
        _context.Set<Listing>().Add(listing);
        _context.SaveChanges();
        return listing;
    }

    public Listing Update(Listing listing)
    {
        _context.Entry(listing).State = EntityState.Modified;
        _context.SaveChanges();
        return listing;
    }

    public void Delete(int id)
    {
        var listing = _context.Set<Listing>().Find(id);
        if (listing != null)
        {
            _context.Set<Listing>().Remove(listing);
            _context.SaveChanges();
        }
    }

    private string ToPriceString(double price)
    {
        return $"${price.ToString("N2")}";
    }
}
