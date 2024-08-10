using Microsoft.AspNetCore.Mvc;
using MainProject.ViewModels;

namespace MainProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Shelf()
        {
            // TODO
            // Insert some dependency injection here after getting the
            // models completely set up.
            ShelfViewModel shelfViewModel = new ShelfViewModel();
            return View(shelfViewModel);
        }
    }
}
