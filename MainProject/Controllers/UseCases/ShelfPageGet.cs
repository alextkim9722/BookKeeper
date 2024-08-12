using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.ViewModels;

namespace MainProject.Controllers.UseCases
{
	public class ShelfPageGet : IShelfPageGet
	{
		private readonly IUserRepository _userRepository;

		public ShelfPageGet(IUserRepository userRepository)
		{
			this._userRepository = userRepository;
		}

		public ShelfPageViewModel createViewModel(int id)
		{
			UserModel user = getUserByID(id);

			ShelfPageViewModel sh = new ShelfPageViewModel()
			{
				profilePicture = user.profile_picture,
				name = user.username,
				pagesRead = 0,
				booksRead = 0,
				joinDate = user.date_joined,
				description = user.description
			};

			return sh;
		}

		private UserModel getUserByID(int id)
			=> _userRepository.getUserById(id);
	}
}
