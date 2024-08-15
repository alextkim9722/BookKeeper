using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.Services.Interfaces;

namespace MainProject.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
			=> _userRepository = userRepository;

		public User getUserModelById(int id)
		{
			throw new NotImplementedException();
		}
	}
}
