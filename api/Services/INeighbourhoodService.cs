using api.Models;

namespace api.Services;

public interface INeighbourhoodService
{
    IEnumerable<Neighbourhood> Get();
}
