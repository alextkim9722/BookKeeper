using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IUserRepository
	{
		public void addUser(User user);
		public void deleteUser(User user);
		public void updateUser(int userId, User user);
		public User getUserByName(String name);
		public User getUserById(int userId);
		public IEnumerable<User> getUsers();
	}
}
