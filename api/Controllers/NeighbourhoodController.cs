using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using api.Services;
using api.Models;
using System.Text.Json;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class NeighbourhoodController : ControllerBase, INeighbourhoodController
{
    private readonly INeighbourhoodService _neighbourhoodService;
    private readonly IDistributedCache _distributedCache;

    public NeighbourhoodController(INeighbourhoodService neighbourhoodService, IDistributedCache distributedCache)
    {
        _neighbourhoodService = neighbourhoodService;
        _distributedCache = distributedCache;
    }

    [HttpGet]
    public IActionResult Get()
    {
        IEnumerable<NeighbourhoodDTO> neighbourhoodDTOs;
        string cachedNeighbourhoodDTOs = _distributedCache.GetString("neighbourhoodDTOs");

        if (!string.IsNullOrEmpty(cachedNeighbourhoodDTOs))
        {
            neighbourhoodDTOs = JsonSerializer.Deserialize<IEnumerable<NeighbourhoodDTO>>(cachedNeighbourhoodDTOs);
        }
        else
        {
            neighbourhoodDTOs = _neighbourhoodService.Get();
            _distributedCache.SetString("neighbourhoodDTOs", JsonSerializer.Serialize(neighbourhoodDTOs));
        }

        return Ok(neighbourhoodDTOs);
    }
}
