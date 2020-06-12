using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Controllers
{
    [ApiController]
    [Route ("api/test")]
    public class TestController : ControllerBase
    {
        public IActionResult GetTest ()
        {
            return Ok ("This is a test controller");
        }
    }
}
