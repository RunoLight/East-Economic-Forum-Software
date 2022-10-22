using Microsoft.AspNetCore.Mvc;

namespace FE_Tourism_Projector.Controllers;

[ApiController]
[Route("")]
public class RootController : ControllerBase
{
    [HttpGet(Name = "")]
    public IActionResult Get()
    {
        return Ok("Everything is fine");
    }
}