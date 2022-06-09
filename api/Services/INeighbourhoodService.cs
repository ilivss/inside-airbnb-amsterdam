using api.Models;

namespace api.Services;

public interface INeighbourhoodService
{
    Task<IEnumerable<Neighbourhood>> Get();
}
