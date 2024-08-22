using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using BackEnd.Model;
using BackEnd.Services.ErrorHandling;
using BackEnd.ErrorHandling;

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

		public async Task<Results<IdentityRole>> createRole(string name)
		{
			IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
			if (result.Succeeded)
			{
				return new ResultsSuccessful<IdentityRole>(null);
			}else
			{
				return new ResultsFailure<IdentityRole>(result.Errors);
			}
		}

		public async Task<Results<IdentityRole>> deleteRole(string name)
		{
			IdentityRole? role = await _roleManager.FindByNameAsync(name);

			if (role != null)
			{
				IdentityResult result = await _roleManager.DeleteAsync(role);
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

		public async Task<Results<IdentityRole>> addToRole(User user, string name)
		{
			Identification? identification = await _userManager.FindByIdAsync(user.identification_id);

			if (identification != null)
			{
				IdentityResult result = await _userManager.AddToRoleAsync(identification, name);
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

		public async Task<Results<IdentityRole>> removeFromRole(User user, string name)
		{
			Identification? identification = await _userManager.FindByIdAsync(user.identification_id);

			if (identification != null)
			{
				IdentityResult result = await _userManager.RemoveFromRoleAsync(identification, name);
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
