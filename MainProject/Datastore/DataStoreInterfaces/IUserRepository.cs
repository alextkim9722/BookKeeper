using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IUserRepository
	{
		void addUser(UserModel user);
		void deleteUser(UserModel user);
		void updateUser(int userId, UserModel user);
		UserModel getUserByName(String name);
		UserModel getUserById(int userId);
		IEnumerable<UserModel> getUsers();
	}
}
