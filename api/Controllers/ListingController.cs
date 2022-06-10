using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
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
    public IActionResult Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    {
        IEnumerable<ListingDTO> listingDTOs;
        string cachedListings = _distributedCache.GetString("listingDTOs");

        if (!string.IsNullOrEmpty(cachedListings))
        {
            listingDTOs = JsonSerializer.Deserialize<IEnumerable<ListingDTO>>(cachedListings);
        }
        else
        {
            listingDTOs = _listingService.Get(minPrice, maxPrice, neighbourhood, minNrOfReviews, maxNrOfReviews);
            _distributedCache.SetString("listingDTOs", JsonSerializer.Serialize(listingDTOs));
        }

        return Ok(listingDTOs);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var listing = _listingService.Get(id);
        return Ok(listing);
    }

    // [HttpPost]
    // public IActionResult Post([FromBody] Listing listing)
    // {
    //     _listingService.Create(listing);
    //     return Ok(listing);
    // }

    // [HttpPut("{id}")]
    // public IActionResult Put(int id, [FromBody] Listing listing)
    // {
    //     listing.Id = id;
    //     _listingService.Update(listing);
    //     return Ok(listing);
    // }

    // [HttpDelete("{id}")]
    // public IActionResult Delete(int id)
    // {
    //     _listingService.Delete(id);
    //     return Ok();
    // }
}
