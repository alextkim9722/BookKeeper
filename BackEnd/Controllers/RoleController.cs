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
			return result.success ? "success" : result.msg;
		}
		[HttpPut("RemoveFromRole/{identificationId}/{name}")]
		public string RemoveFromRole(string identificationId, string name)
		{
			var result = _roleService.RemoveFromRole(identificationId, name);
			return result.success ? "success" : result.msg;
		}
		[HttpPut("AddtoRole/{identificationId}/{name}")]
		public string AddtoRole(string identificationId, string namee)
		{
			var result = _roleService.AddToRole(identificationId, namee);
			return result.success ? "success" : result.msg;
		}
		[HttpDelete("DeleteRole/{name}")]
		public string DeleteRole(string name)
		{
			var result = _roleService.DeleteRole(name);
			return result.success ? "success" : result.msg;
		}
	}
}
