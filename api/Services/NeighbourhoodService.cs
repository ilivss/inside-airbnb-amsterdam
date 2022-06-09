using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

 public class NeighbourhoodService : INeighbourhoodService
 {
     private readonly ApplicationContext _context;

     public NeighbourhoodService(ApplicationContext context) {
            _context = context;
     }

    public async Task<IEnumerable<Neighbourhood>> Get()
    {
        // De geven dataset (AirBNB.bacpac) klopt voor geen meter!!!
        // De neighbourhoods records komen niet overeen met de 'neighbourhoods' column van Listing. 
        // return _context.Neighbourhoods.ToList();

        // Workaround:
        return await _context.Listings
                    .AsNoTracking()
                    .Select(l => new Neighbourhood(){Name = l.Neighbourhood})
                    .Distinct()
                    .ToListAsync();
    }
}
