using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Models;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ListingController : ControllerBase, IListingController
{
    private readonly IListingService _listingService;

    public ListingController(IListingService listingService)
    {
        _listingService = listingService;
    }

    [HttpGet]
    public IActionResult Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    {
        var listings = _listingService.Get(minPrice, maxPrice, neighbourhood, minNrOfReviews, maxNrOfReviews);

        return Ok(listings);
    }

    [HttpGet("Location")]
    public IActionResult GetLocations(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    {
        var listings = _listingService.Get(minPrice, maxPrice, neighbourhood, minNrOfReviews, maxNrOfReviews);
        var locations = listings.Select(l => new { l.Id, l.Name, l.Latitude, l.Longitude, l.RoomType });
        return Ok(locations);
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
