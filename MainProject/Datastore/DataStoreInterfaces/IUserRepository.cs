using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IUserRepository
	{
		public void addUser(UserModel user);
		public void deleteUser(UserModel user);
		public void updateUser(int userId, UserModel user);
		public UserModel getUserByName(String name);
		public UserModel getUserById(int userId);
		public IEnumerable<UserModel> getUsers();
	}
}
