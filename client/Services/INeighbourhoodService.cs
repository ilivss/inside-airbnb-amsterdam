using client.Models;

namespace client.Services;

public interface INeighbourhoodService
{
    Task<List<Neighbourhood>?> Get();
}
