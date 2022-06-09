using api.Models;

namespace api.Services;

public interface INeighbourhoodService
{
    Task<IEnumerable<NeighbourhoodDTO>> Get();
}
