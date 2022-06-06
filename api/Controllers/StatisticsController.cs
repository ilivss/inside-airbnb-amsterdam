using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class StatisticsController : ControllerBase, IStatisticsController
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("ListingCountPerNeighbourhood")]
    public IActionResult Get()
    {
        var statistics = _statisticsService.GetListingCountPerNeighbourhood();
        return Ok(statistics);
    }

    [HttpGet("AveragePricePerNeighbourhood")]
    public IActionResult GetAveragePricePerNeighbourhood()
    {
        var statistics = _statisticsService.GetAveragePricePerNeighbourhood();
        return Ok(statistics);
    }

    [HttpGet("AverageAvailabilityPerNeighbourhood")]
    public IActionResult GetAverageAvailabilityPerNeighbourhood()
    {
        var statistics = _statisticsService.GetAverageAvailabilityPerNeighbourhood();
        return Ok(statistics);
    }

    [HttpGet("AverageBedsPerNeighbourhood")]
    public IActionResult GetAverageBedsPerNeighbourhood()
    {
        var statistics = _statisticsService.GetAverageBedsPerNeighbourhood();
        return Ok(statistics);
    }
}
