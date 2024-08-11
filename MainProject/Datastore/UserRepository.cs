using System.Threading.Tasks;
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
			bookShelfContext.users.Add(userModel);
			bookShelfContext.SaveChanges();
		}

		public void deleteUser(int userId)
		{
			throw new NotImplementedException();
		}

		public UserModel getUserById(int userId)
		{
			return bookShelfContext.users.Find(userId);
		}

		public UserModel getUserByName(string name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<UserModel> getUsers()
		{
			throw new NotImplementedException();
		}

		public void updateUser(int userId, UserModel user)
		{
			throw new NotImplementedException();
		}
	}
}
