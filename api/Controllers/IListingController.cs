using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Models;

namespace api.Controllers;

public interface IListingController
{
    // IActionResult Delete(int id);
    Task<IActionResult> Get(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews);
    Task<IActionResult> Get(int id);
    // Task<IActionResult> GetLocations(int? minPrice, int? maxPrice, string? neighbourhood, int? minNrOfReviews, int? maxNrOfReviews);
    // Task<IActionResult> Post([FromBody] Listing listing);
    // Task<IActionResult> Put(int id, [FromBody] Listing listing);
}
