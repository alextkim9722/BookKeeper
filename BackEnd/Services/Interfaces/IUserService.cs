using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services.Interfaces
{
	public interface IUserService
	{
		public Results<User> addUser(User user);
		public Results<User> removeUser(int id);
		public Results<User> updateUser(int id, User user);
		public Results<User> getUserById(int id);
		public Results<User> getUserByIdentificationId(string id);
		public Results<User> getUserByUserName(string userName);
		public Results<IEnumerable<User>> getAllUser();
	}
}
