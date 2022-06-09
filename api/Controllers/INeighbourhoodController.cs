using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public interface INeighbourhoodController
{
    Task<IActionResult> Get();
}
