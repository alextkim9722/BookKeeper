using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BackEnd.Services
{
	public class IdentificationService : IIdentificationService
	{
		private readonly UserManager<Identification> _userManager;
		private readonly IPasswordHasher<Identification> _passwordHasher;
		private readonly SignInManager<IdentityUser> _signInManager;

		private readonly IUserService _userService;

		private readonly string defaultDescription = string.Empty;
		private readonly string defaultProfilePicPath = string.Empty;

		public IdentificationService(
			UserManager<Identification> userManager,
			IPasswordHasher<Identification> passwordHasher,
			IUserService userService,
			SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_passwordHasher = passwordHasher;
			_userService = userService;

			defaultDescription = "Hi! I just got here!";
			defaultProfilePicPath = "PATH TO IMAGE";
			_signInManager = signInManager;
		}

		public async Task<Identification>? createIdentification(
			Identification identification, string password)
		{
			if (identification != null)
			{
				IdentityResult result = await _userManager
					.CreateAsync(identification, password);

				if (result.Succeeded)
				{
					User user = new User
					{
						identification_id = identification.Id,
						username = identification.UserName!,
						date_joined = DateOnly.FromDateTime(DateTime.Now),
						description = defaultProfilePicPath
					};

					_userService.addUser(user);

					return identification;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		public async Task<Identification>? login(string input, string type, string password)
		{
			Identification? identification = null;

			switch (type)
			{
				case "email":
					identification = await getIdentificationByEmail(input);
					break;
				case "username":
					identification = await getIdentificationByUsername(input);
					break;
				default:
					return null;
					break;
			}

			await _signInManager.SignOutAsync();
			SignInResult result = await _signInManager.PasswordSignInAsync(identification, password, false, false);

			return identification;
		}

		public async Task<Identification>? getIdentificationByEmail(string email)
		{
			Identification? identification = await _userManager.FindByEmailAsync(email);
			identification = addUserId(identification);
			return identification;
		}

		public async Task<Identification>? getIdentificationByUsername(string username)
		{
			Identification? identification = await _userManager.FindByNameAsync(username);
			identification = addUserId(identification);
			return identification;
		}

		private Identification? addUserId(Identification? identification)
		{
			if (identification != null)
			{
				identification.user_id = _userService.getUserByIdentificationId(identification.Id).user_id;
			}

			return identification;
		}

		public async Task<Identification>? removeIdentification(string id)
		{
			Identification? identification = await _userManager.FindByIdAsync(id);
			if (identification != null)
			{
				_userService.removeUser(identification.user_id);
				await _userManager.DeleteAsync(identification);
				return identification;
			}
			else
			{
				return null;
			}
		}

		public async Task<Identification>? updateIdentification(
			string id, Identification? identification)
		{
			IdentityResult result = await _userManager.UpdateAsync(identification);

			if (result.Succeeded)
			{
				return identification;
			}
			else
			{
				return null;
			}
		}
	}
}
