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
    public async Task<IActionResult> Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews)
    {
        var listingDTOs = await _listingService.Get(minPrice, maxPrice, neighbourhood, minNrOfReviews, maxNrOfReviews);

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
