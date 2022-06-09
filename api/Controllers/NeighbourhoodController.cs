using System.Text.Json;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

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
    public async Task<IActionResult> Get()
    {
        IEnumerable<NeighbourhoodDTO> neighbourhoodDTOs;
        string cachedNeighbourhoodDTOs = await _distributedCache.GetStringAsync("neighbourhoodDTOs");

        if(!string.IsNullOrEmpty(cachedNeighbourhoodDTOs))
        {
            neighbourhoodDTOs = JsonSerializer.Deserialize<IEnumerable<NeighbourhoodDTO>>(cachedNeighbourhoodDTOs);
        }
        else
        {
            neighbourhoodDTOs = await _neighbourhoodService.Get();
            await _distributedCache.SetStringAsync("neighbourhoodDTOs", JsonSerializer.Serialize(neighbourhoodDTOs));
        }

        return Ok(neighbourhoodDTOs);
    }
}
