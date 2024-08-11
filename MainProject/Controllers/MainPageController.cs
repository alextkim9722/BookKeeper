using Microsoft.AspNetCore.Mvc;
using MainProject.Controllers.UseCases;
using MainProject.ViewModels;
using MainProject.Model;

namespace MainProject.Controllers
{
    public class MainPageController : Controller
    {
        private const int user_id = 3;
        private readonly IShelfPageGet shelfPageGet;

        public MainPageController(
            IShelfPageGet shelfPageGet)
        {
            this.shelfPageGet = shelfPageGet;
        }

        [HttpGet]
        public IActionResult ShelfPage()
        {
            ShelfPageViewModel shelfPageViewModel = shelfPageGet.createViewModel(user_id);
			return View(shelfPageViewModel);
        }
    }
}
