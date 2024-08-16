using BackEnd.Model;
using BackEnd.Services.Abstracts;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
	public class UserService : JoinServiceAbstract<User>, IUserService
	{
		private readonly Callback handler1;

		public UserService(BookShelfContext bookShelfContext) :
			base(bookShelfContext)
		{
			handler1 = analyseBooks;
			CallbackHandler += handler1;
		}

		public User? addUser(User user)
			=> addModel(user);

		public IEnumerable<User>? getAllUser()
			=> getAllModels();

		public User? getUserById(int id)
			=> formatModel(CallbackHandler, null, x => x.user_id == id);

		public User? getUserByUserName(string userName)
			=> formatModel(CallbackHandler, null, x => x.username == userName);

		public User? removeUser(int id)
		{
			if (deleteBridges(id))
			{
				return deleteModel(x => x.user_id == id);
			}
			else
			{
				return null;
			}
		}

		public User? updateUser(int id, User user)
		{
			User? updatedUser = updateModel(id, user);

			if (updatedUser != null)
			{
				updatedUser.pagesRead = user.pagesRead;
				updatedUser.booksRead = user.booksRead;
				updatedUser.books = user.books;
			}

			return updatedUser;
		}

		private User? analyseBooks(User user)
		{
			user = addBridges(user);
			user = recordBookData(user);

			return user;
		}

		private User recordBookData(User user)
		{
			if(user != null)
			{
				user.pagesRead = user.books.Select(x => x.pages).Sum();
				user.booksRead = user.books.Count();
			}

			return user;
		}

		protected override User addBridges(User user)
		{
			user.books = getMultipleJoins<Book, User_Book>(
				x => x.user_id == user.user_id, y => y.book_id);
			user.reviews = getMultipleJoins<Review, User_Review>(
				x => x.user_id == user.user_id, y => y.review_id);

			return user;
		}

		protected override bool deleteBridges(int id)
		{
			return
				deleteJoins<User_Book>(x => x.user_id == id);
		}
	}
}
