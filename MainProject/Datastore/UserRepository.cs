using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;

namespace MainProject.Datastore
{
	public class UserRepository : IUserRepository
	{
		private readonly BookShelfContext bookShelfContext;

		public UserRepository(BookShelfContext bookShelfContext)
		{
			this.bookShelfContext = bookShelfContext;
		}

		public void addUser(UserModel userModel)
		{
			bookShelfContext.User.Add(userModel);
			bookShelfContext.SaveChanges();
		}

		public void deleteUser(UserModel user)
		{
			bookShelfContext.User.Remove(user);
			bookShelfContext.SaveChanges();
		}

		public UserModel getUserById(int userId)
		{
			return bookShelfContext.User.Find(userId);
		}

		public UserModel getUserByName(string name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<UserModel> getUsers()
		{
			return bookShelfContext.User.ToList();
		}

		public void updateUser(int userId, UserModel user)
		{
			if (userId != user.user_id) return;

			var user_target = bookShelfContext.User.Find(userId);
			if (user_target == null) return;

			user_target.username = user.username;
			user_target.description = user.description;
			bookShelfContext.SaveChanges();
		}
	}
}
