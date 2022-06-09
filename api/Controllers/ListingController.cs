using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

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
        IEnumerable<Listing> listings;
        string cachedListings = await _distributedCache.GetStringAsync("listings");

        if (!string.IsNullOrEmpty(cachedListings))
        {
            listings = JsonSerializer.Deserialize<IEnumerable<Listing>>(cachedListings);
        }
        else {
            listings = await _listingService.Get(minPrice, maxPrice, neighbourhood, minNrOfReviews, maxNrOfReviews);
            await _distributedCache.SetStringAsync("listings", JsonSerializer.Serialize(listings));
        }

        return Ok(listings);
    }

    [HttpGet("Location")]
    public async Task<IActionResult> GetLocations(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    {
        IEnumerable<Listing> listings;
        string cachedListings = await _distributedCache.GetStringAsync("listings");

        if (!string.IsNullOrEmpty(cachedListings))
        {
            listings = JsonSerializer.Deserialize<IEnumerable<Listing>>(cachedListings);
        }
        else {
            listings = await _listingService.Get(minPrice, maxPrice, neighbourhood, minNrOfReviews, maxNrOfReviews);
            await _distributedCache.SetStringAsync("listings", JsonSerializer.Serialize(listings));
        }
        
        var locations = listings.Select(l => new { l.Id, l.Name, l.Latitude, l.Longitude, l.RoomType });
        return Ok(locations);
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
