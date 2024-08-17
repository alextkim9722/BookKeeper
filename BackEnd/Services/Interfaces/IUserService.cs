using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
	public interface IUserService
	{
		public User? addUser(User user);
		public User? removeUser(int id);
		public User? updateUser(int id, User user);
		public User? getUserById(int id);
		public User? getUserByIdentificationId(string id);
		public User? getUserByUserName(string userName);
		public IEnumerable<User>? getAllUser();
	}
}
