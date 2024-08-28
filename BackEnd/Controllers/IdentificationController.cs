using BackEnd.Model;
using BackEnd.Services;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentificationController : ControllerBase
	{
		private readonly IIdentificationService _identificationService;
		private readonly SignInManager<Identification> _signInManager;
		private readonly IUserService _userService;

		public IdentificationController(IIdentificationService identificationService, SignInManager<Identification> signInManager, IUserService userService)
		{
			_identificationService = identificationService;
			_signInManager = signInManager;
			_userService = userService;
		}

		[HttpPost("CreateIdentification/{identificationJson}/{password}")]
		public string CreateIdentification(string identificationJson, string password)
		{
			var identification = JsonConvert.DeserializeObject<Identification>(identificationJson);
			var results = _identificationService.createIdentification(identification, password);
			return JsonConvert.SerializeObject(results);
		}
		[HttpPost("LoginIdentification/{username}/{password}")]
		public string LoginIdentification(string username, string password)
		{
			var userResult = _identificationService.getIdentificationByUsername(username);
			if (!userResult.success) return JsonConvert.SerializeObject(userResult);

			var results = Task.Run(() => _signInManager.PasswordSignInAsync(userResult.payload, password, false, false)).GetAwaiter().GetResult();
			return JsonConvert.SerializeObject(results);
		}
		[HttpPut("UpdateIdentification/{identificationJson}")]
		public string UpdateIdentification(string identificationJson)
		{
			var identification = JsonConvert.DeserializeObject<Identification>(identificationJson);
			var result = _identificationService.updateIdentification(identification);
			return JsonConvert.SerializeObject(result);
		}
		[HttpPut("SignOut")]
		public string SignOut(string identificationJson)
		{
			Task.Run(() => _signInManager.SignOutAsync()).GetAwaiter().GetResult();
			return "success";
		}
		[HttpDelete("DeleteIdentification/{identification}")]
		public string DeleteIdentification(string identification)
		{
			var result = _identificationService.removeIdentification(identification);
			return JsonConvert.SerializeObject(result);
		}
	}
}
