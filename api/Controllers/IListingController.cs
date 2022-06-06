using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Models;

namespace api.Controllers;

public interface IListingController
{
    // IActionResult Delete(int id);
    IActionResult Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews);
    IActionResult Get(int id);
    // IActionResult GetLocations(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews);
    // IActionResult Post([FromBody] Listing listing);
    // IActionResult Put(int id, [FromBody] Listing listing);
}
