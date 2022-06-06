using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public interface IStatisticsController
{
    IActionResult Get();
    IActionResult GetAverageAvailabilityPerNeighbourhood();
    IActionResult GetAverageBedsPerNeighbourhood();
    IActionResult GetAveragePricePerNeighbourhood();
}
