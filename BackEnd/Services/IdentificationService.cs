using BackEnd.Model;
using BackEnd.Services.Interfaces;
using BackEnd.Services.ErrorHandling;
using Microsoft.AspNetCore.Identity;


namespace BackEnd.Services
{
	public class IdentificationService : IIdentificationService
	{
		private readonly UserManager<Identification> _userManager;
		private readonly IUserService _userService;

		public IdentificationService(UserManager<Identification> userManager, IUserService userService)
		{
			_userManager = userManager;
			_userService = userService;
		}

		public Results<Identification> createIdentification(Identification identification, string password)
		{
			if (identification != null)
			{
				IdentityResult result = Task.Run(() => _userManager.CreateAsync(identification, password)).GetAwaiter().GetResult();

				if (result.Succeeded)
				{
					User user = new User
					{
						identification_id = identification.Id,
						username = identification.UserName!,
						date_joined = DateOnly.FromDateTime(DateTime.Now)
					};

					Results<User> userResult = _userService.AddUser(user);

					if(userResult.success)
					{
						return new ResultsSuccessful<Identification>(identification);
					}
					else
					{
						return new ResultsFailure<Identification>(userResult.msg);
					}
				}
				else
				{
					return new ResultsFailure<Identification>(result.Errors);
				}
			}
			else
			{
				return new ResultsFailure<Identification>("Identification is null");
			}
		}

		public Results<Identification> getIdentificationByEmail(string email)
		{
			Identification? identification = Task.Run(() => _userManager.FindByEmailAsync(email)).GetAwaiter().GetResult();
			if(identification != null)
			{
				return new ResultsSuccessful<Identification>(identification);
			}else
			{
				return new ResultsFailure<Identification>("Email is not found");
			}
		}

		public Results<Identification> getIdentificationByUsername(string username)
		{
			Identification? identification = Task.Run(() => _userManager.FindByNameAsync(username)).GetAwaiter().GetResult();
			if (identification != null)
			{
				return new ResultsSuccessful<Identification>(identification);
			}
			else
			{
				return new ResultsFailure<Identification>("Username is not found");
			}
		}

		public Results<Identification> updateIdentification(Identification identification)
		{
			IdentityResult result = Task.Run(() => _userManager.UpdateAsync(identification)).GetAwaiter().GetResult();

			if (result.Succeeded)
			{
				return new ResultsSuccessful<Identification>(identification);
			}
			else
			{
				return new ResultsFailure<Identification>(result.Errors);
			}
		}

		public Results<Identification> removeIdentification(string id)
		{
			Identification identification = Task.Run(() => _userManager.FindByIdAsync(id)).GetAwaiter().GetResult();
			if (identification != null)
			{
				_userService.RemoveUser([identification.user_id]);
				Task.Run(() => _userManager.DeleteAsync(identification));
				return new ResultsSuccessful<Identification>(identification);
			}
			else
			{
				return new ResultsFailure<Identification>("Failed to remove identification.");
			}
		}
	}
}
