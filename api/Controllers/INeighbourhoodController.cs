using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public interface INeighbourhoodController
{
    IActionResult Get();
}
