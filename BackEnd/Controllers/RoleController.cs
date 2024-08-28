using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackEnd.Controllers
{
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[HttpPost("CreateRole/{name}")]
		public string CreateRole(string name)
		{
			var result = _roleService.CreateRole(name);
			return JsonConvert.SerializeObject(result);
		}
		[HttpPut("RemoveFromRole/{identificationId}/{name}")]
		public string RemoveFromRole(string identificationId, string name)
		{
			var result = _roleService.RemoveFromRole(identificationId, name);
			return JsonConvert.SerializeObject(result);
		}
		[HttpPut("AddtoRole/{identificationId}/{name}")]
		public string AddtoRole(string identificationId, string namee)
		{
			var result = _roleService.AddToRole(identificationId, namee);
			return JsonConvert.SerializeObject(result);
		}
		[HttpDelete("DeleteRole/{name}")]
		public string DeleteRole(string name)
		{
			var result = _roleService.DeleteRole(name);
			return JsonConvert.SerializeObject(result);
		}
	}
}
