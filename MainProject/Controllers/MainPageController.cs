using Microsoft.AspNetCore.Mvc;
using MainProject.Controllers.UseCases;
using MainProject.ViewModels;
using MainProject.Model;

namespace MainProject.Controllers
{
    public class MainPageController : Controller
    {
        private const int _user_id = 3; // Only exists temporarily
        private readonly IShelfPageGet _shelfPageGet;

        public MainPageController(
            IShelfPageGet shelfPageGet)
        {
            this._shelfPageGet = shelfPageGet;
        }

        [HttpGet]
        public IActionResult ShelfPage()
        {
            ShelfPageViewModel shelfPageViewModel = _shelfPageGet.createViewModel(_user_id);
			return View(shelfPageViewModel);
        }
    }
}
