using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreFeatures.Hosts.WebApplications
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Ankk");
        }
    }
}
