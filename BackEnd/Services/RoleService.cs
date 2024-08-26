using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<Identification> _userManager;

		public RoleService(
			UserManager<Identification> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public Results<IdentityRole> CreateRole(string name)
		{
			IdentityResult result = Task.Run(() => _roleManager.CreateAsync(new IdentityRole(name))).GetAwaiter().GetResult();
			if (result.Succeeded)
			{
				return new ResultsSuccessful<IdentityRole>(null);
			}else
			{
				return new ResultsFailure<IdentityRole>(result.Errors);
			}
		}

		public Results<IdentityRole> DeleteRole(string name)
		{
			IdentityRole? role = Task.Run(() => _roleManager.FindByNameAsync(name)).GetAwaiter().GetResult();

			if (role != null)
			{
				IdentityResult result = Task.Run(() => _roleManager.DeleteAsync(role)).GetAwaiter().GetResult();
				if (result.Succeeded)
				{
					return new ResultsSuccessful<IdentityRole>(null);
				}
				else
				{
					return new ResultsFailure<IdentityRole>(result.Errors);
				}
			}
			else
			{
				return new ResultsFailure<IdentityRole>("Role was not found");
			}
		}

		public Results<IdentityRole> AddToRole(string identificationId, string name)
		{
			Identification? identification = Task.Run(() => _userManager.FindByIdAsync(identificationId)).GetAwaiter().GetResult();

			if (identification != null)
			{
				IdentityResult result = Task.Run(() => _userManager.AddToRoleAsync(identification, name)).GetAwaiter().GetResult();
				if (result.Succeeded)
				{
					return new ResultsSuccessful<IdentityRole>(null);
				}
				else
				{
					return new ResultsFailure<IdentityRole>("Could not add user to role");
				}
			}
			else
			{
				return new ResultsFailure<IdentityRole>("Could not find user identification");
			}
		}

		public Results<IdentityRole> RemoveFromRole(string identificationId, string name)
		{
			Identification? identification = Task.Run(() => _userManager.FindByIdAsync(identificationId)).GetAwaiter().GetResult();

			if (identification != null)
			{
				IdentityResult result = Task.Run(() => _userManager.RemoveFromRoleAsync(identification, name)).GetAwaiter().GetResult();
				if (result.Succeeded)
				{
					return new ResultsSuccessful<IdentityRole>(null);
				}
				else
				{
					return new ResultsFailure<IdentityRole>("Could not remove user from role");
				}
			}
			else
			{
				return new ResultsFailure<IdentityRole>("Could not find user identification");
			}
		}
	}
}
