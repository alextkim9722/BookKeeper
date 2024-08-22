using BackEnd.Model;
using BackEnd.Services.Interfaces;
using BackEnd.Services.ErrorHandling;
using Microsoft.AspNetCore.Identity;
using BackEnd.ErrorHandling;


namespace BackEnd.Services
{
    public class IdentificationService : IIdentificationService
	{
		private readonly UserManager<Identification> _userManager;

		private readonly IPasswordHasher<Identification> _passwordHasher;
		private readonly IUserService _userService;

		public IdentificationService(
			UserManager<Identification> userManager,
			IPasswordHasher<Identification> passwordHasher,
			IUserService userService)
		{
			_userManager = userManager;
			_passwordHasher = passwordHasher;
			_userService = userService;
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
						date_joined = DateOnly.FromDateTime(DateTime.Now)
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
					return new ResultsFailure<Identification>(result.Errors);
				}
			}
			else
			{
				return new ResultsFailure<Identification>(
						"Identification is null");
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
			var result = _userService.getUserByIdentificationId(identification.Id);
			if (result.success)
			{
				identification.user_id = result.payload!.user_id;
				return new ResultsSuccessful<Identification>(
					identification);
			}
			else
			{
				return new ResultsFailure<Identification>(
					result.msg + "Error with grabbing UserId");
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
				return new ResultsFailure<Identification>("Failed to remove identification.");
			}
		}

		public async Task<Results<Identification>> updateIdentification(
			Identification identification)
		{
			IdentityResult result = await _userManager.UpdateAsync(identification);

			if (result.Succeeded)
			{
				return new ResultsSuccessful<Identification>(identification);
			}
			else
			{
				return new ResultsFailure<Identification>(result.Errors);
			}
		}
	}
}
