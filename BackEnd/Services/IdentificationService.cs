using BackEnd.Model;
using BackEnd.Services.Interfaces;
using BackEnd.Services.ErrorHandling;
using Microsoft.AspNetCore.Identity;


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

		public async Task<Results<Identification>> createIdentification(
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

					Results<User> userResult = _userService.addUser(user);

					if(userResult.success)
					{
						return new ResultsSuccessful<Identification>(identification);
					}
					else
					{
						return new ResultsFailure<Identification>(
							userResult.msg
							+ "Identity User creation for failed"
							);
					}
				}
				else
				{
					return new ResultsFailure<Identification>(
						"Identity creation has failed");
				}
			}
			else
			{
				return new ResultsFailure<Identification>(
						"Identification is null");
			}
		}

		public async Task<Results<Identification>> login(string input, string type, string password)
		{
			Results<Identification> identification;

			switch (type)
			{
				case "email":
					identification = await getIdentificationByEmail(input);
					break;
				case "username":
					identification = await getIdentificationByUsername(input);
					break;
				default:
					return new ResultsFailure<Identification>("Incorrect login type");
			}

			if(identification.success){
				await _signInManager.SignOutAsync();
				SignInResult result = await _signInManager.PasswordSignInAsync(
					identification.payload!, password, false, false);

				return new ResultsSuccessful<Identification>(identification.payload);
			}
			else
			{
				return new ResultsFailure<Identification>(
					identification.msg
					+ "Login has failed");
			}
		}

		public async Task<Results<Identification>> getIdentificationByEmail(string email)
		{
			Identification? identification = await _userManager.FindByEmailAsync(email);
			if(identification == null)
			{
				return new ResultsSuccessful<Identification>(identification);
			}else
			{
				return new ResultsFailure<Identification>("Email is not found");
			}
		}

		public async Task<Results<Identification>> getIdentificationByUsername(string username)
		{
			Identification? identification = await _userManager.FindByNameAsync(username);
			if (identification == null)
			{
				return new ResultsSuccessful<Identification>(identification);
			}
			else
			{
				return new ResultsFailure<Identification>("Username is not found");
			}
		}

		private Results<Identification> addUserId(Identification identification)
		{
			var result = _userService.getUserByIdentificationId(
				identification.Id);
			if (result.success)
			{
				identification.user_id = result.payload!.user_id;
				return new ResultsSuccessful<Identification>(
					identification);
			}
			else
			{
				return new ResultsFailure<Identification>(
					"User has not found by identification Id");
			}
		}

		public async Task<Results<Identification>> removeIdentification(string id)
		{
			Identification identification = await _userManager.FindByIdAsync(id);
			if (identification != null)
			{
				_userService.removeUser(identification.user_id);
				await _userManager.DeleteAsync(identification);
				return new ResultsSuccessful<Identification>(
					identification);
			}
			else
			{
				return new ResultsFailure<Identification>(
					"Removing Identification has failed");
			}
		}

		public async Task<Results<Identification>> updateIdentification(
			Identification identification)
		{
			IdentityResult result = await _userManager
				.UpdateAsync(identification);

			if (result.Succeeded)
			{
				return new ResultsSuccessful<Identification>(
					identification);
			}
			else
			{
				return new ResultsFailure<Identification>(
					"Updating Identification has failed");
			}
		}
	}
}
