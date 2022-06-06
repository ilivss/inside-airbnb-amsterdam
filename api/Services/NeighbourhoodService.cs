using api.Models;

namespace api.Services;

 public class NeighbourhoodService : INeighbourhoodService
 {
     private readonly ApplicationContext _context;

     public NeighbourhoodService(ApplicationContext context) {
            _context = context;
     }

    public IEnumerable<Neighbourhood> Get()
    {
        // De geven dataset (AirBNB.bacpac) klopt voor geen meter!!!
        // De neighbourhoods records komen niet overeen met de 'neighbourhoods' column van Listing. 
        // return _context.Neighbourhoods.ToList();

        // Workaround:
        return _context.Listings.Select(l => new Neighbourhood(){Name = l.Neighbourhood}).Distinct().ToList();
    }
}
