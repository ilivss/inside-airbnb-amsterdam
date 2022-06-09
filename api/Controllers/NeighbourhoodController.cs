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
        IEnumerable<Neighbourhood> neighbourhoods;
        string cachedNeighbourhoods = await _distributedCache.GetStringAsync("neighbourhoods");

        if(!string.IsNullOrEmpty(cachedNeighbourhoods))
        {
            neighbourhoods = JsonSerializer.Deserialize<IEnumerable<Neighbourhood>>(cachedNeighbourhoods);
        }
        else
        {
            neighbourhoods = await _neighbourhoodService.Get();
            await _distributedCache.SetStringAsync("neighbourhoods", JsonSerializer.Serialize(neighbourhoods));
        }

        return Ok(neighbourhoods);
    }
}
