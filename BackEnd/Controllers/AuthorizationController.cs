using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Model;

namespace BackEnd.Controllers
{
	public class AuthorizationController : Controller
	{

		public AuthorizationController() { }

		public IActionResult Index()
		{
			return View();
		}
	}
}
