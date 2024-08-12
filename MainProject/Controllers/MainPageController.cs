using Microsoft.AspNetCore.Mvc;
using MainProject.ViewModel;
using MainProject.Model;
using MainProject.Services.Interfaces;

namespace MainProject.Controllers
{
    public class MainPageController : Controller
    {
        private const int _user_id = 3; // Only exists temporarily
        private readonly IShelfPageService _shelfPageGet;

        public MainPageController(
            IShelfPageService shelfPageGet)
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
