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
        IEnumerable<NeighbourhoodDTO> neighbourhoods;
        string cachedNeighbourhoods = await _distributedCache.GetStringAsync("neighbourhoodDTOs");

        if(!string.IsNullOrEmpty(cachedNeighbourhoods))
        {
            neighbourhoods = JsonSerializer.Deserialize<IEnumerable<NeighbourhoodDTO>>(cachedNeighbourhoods);
        }
        else
        {
            neighbourhoods = await _neighbourhoodService.Get();
            await _distributedCache.SetStringAsync("neighbourhoodDTOs", JsonSerializer.Serialize(neighbourhoods));
        }

        return Ok(neighbourhoods);
    }
}
