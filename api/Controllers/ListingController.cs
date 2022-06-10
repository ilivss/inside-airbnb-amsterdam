using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json;
using api.Models;
using api.Services;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ListingController : ControllerBase, IListingController
{
    private readonly IListingService _listingService;
    private readonly IDistributedCache _distributedCache;

    public ListingController(IListingService listingService, IDistributedCache distributedCache)
    {
        _listingService = listingService;
        _distributedCache = distributedCache;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    {
        IEnumerable<ListingDTO> listingDTOs;
        string cachedListings = await _distributedCache.GetStringAsync("listingDTOs");

        if (!string.IsNullOrEmpty(cachedListings))
        {
            listingDTOs = JsonSerializer.Deserialize<IEnumerable<ListingDTO>>(cachedListings);
        }
        else {
            listingDTOs = await _listingService.Get(minPrice, maxPrice, neighbourhood, minNrOfReviews, maxNrOfReviews);
            await _distributedCache.SetStringAsync("listingDTOs", JsonSerializer.Serialize(listingDTOs));
        }

        return Ok(listingDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var listing = await _listingService.Get(id);
        return Ok(listing);
    }

    // [HttpPost]
    // public async Task<IActionResult> Post([FromBody] Listing listing)
    // {
    //     await _listingService.Create(listing);
    //     return Ok(listing);
    // }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put(int id, [FromBody] Listing listing)
    // {
    //     listing.Id = id;
    //     await _listingService.Update(listing);
    //     return Ok(listing);
    // }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     await _listingService.Delete(id);
    //     return Ok();
    // }
}
