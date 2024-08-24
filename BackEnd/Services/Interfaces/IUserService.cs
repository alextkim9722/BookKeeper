using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services.Interfaces
{
	public interface IUserService
	{
		public Results<User> AddUser(User user);
		public Results<IEnumerable<User>> RemoveUser(IEnumerable<int> id);
		public Results<User> UpdateUser(int id, User user);
		public Results<User> GetUserById(int id);
		public Results<User> GetUserByIdentificationId(string id);
		public Results<IEnumerable<User>> GetUserByUserName(string username);
		public Results<IEnumerable<Book>> SetBooksAndPagesRead(User user, IEnumerable<Book> books);
	}
}
