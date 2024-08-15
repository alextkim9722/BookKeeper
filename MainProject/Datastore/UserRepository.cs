using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Datastore
{
	public class UserRepository : IUserRepository
	{
		private readonly BookShelfContext _context;

		public UserRepository(BookShelfContext context)
			=>this._context = context;

		public void addUser(User userModel)
		{
			_context.User.Add(userModel);
			_context.SaveChanges();
		}

		public void deleteUser(User user)
		{
			_context.User.Remove(user);
			_context.SaveChanges();
		}

		public User getUserById(int userId)
			=> _context.User.Find(userId);

		public User getUserByName(string name)
			=> _context.User.Where(x => x.username == name).FirstOrDefault();

		public IEnumerable<User> getUsers()
			=> _context.User.ToList();

		public void updateUser(int userId, User user)
		{
			if (userId != user.user_id) return;

			var user_target = _context.User.Find(userId);
			if (user_target == null) return;

			user_target.username = user.username;
			user_target.description = user.description;
			user_target.date_joined = user.date_joined;
			user_target.profile_picture = user.profile_picture;
			_context.SaveChanges();
		}
	}
}
