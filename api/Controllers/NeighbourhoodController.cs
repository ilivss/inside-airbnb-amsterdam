using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class NeighbourhoodController : ControllerBase, INeighbourhoodController
{
    private readonly INeighbourhoodService _neighbourhoodService;

    public NeighbourhoodController(INeighbourhoodService neighbourhoodService)
    {
        _neighbourhoodService = neighbourhoodService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var neighbourhoodDTOs = await _neighbourhoodService.Get();
        return Ok(neighbourhoodDTOs);
    }
}
