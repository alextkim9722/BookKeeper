using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainPageController : ControllerBase
    {
        public MainPageController()
        {}

        [HttpGet]
        public string ShelfPage()
        {
            return "hello";
        }
    }
}
