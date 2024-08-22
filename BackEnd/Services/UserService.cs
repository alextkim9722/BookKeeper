using BackEnd.Model;
using BackEnd.Services.Interfaces;
using BackEnd.Services.ErrorHandling;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.IdentityModel.Tokens;
using BackEnd.ErrorHandling;

namespace BackEnd.Services
{
    public class UserService : JoinService<User>, IUserService
	{
		public UserService(BookShelfContext bookShelfContext) :
			base(bookShelfContext)
		{
			CallbackHandler.Add(addBridges);
			CallbackHandler.Add(recordBookData);
		}

		public Results<User> addUser(User user)
			=> addModel(user);

		public Results<IEnumerable<User>> getAllUser()
			=> formatAllModels();

		public Results<User> getUserById(int id)
			=> formatModel(x => x.user_id == id);

		public Results<User> getUserByIdentificationId(string id)
			=> formatModel(x => x.identification_id == id);

		public Results<User> getUserByUserName(string userName)
			=> formatModel(x => x.username == userName);

		public Results<User> removeUser(int id)
			=> deleteBridgedModel(id, x => x.user_id == id);

		public Results<User> updateUser(int id, User user)
			=> updateModel(x => x.user_id == id, user);

		private Results<User> recordBookData(User user)
		{
			if(!user.books.IsNullOrEmpty())
			{
				user.pagesRead = user.books!.Select(x => x.pages).Sum();
				user.booksRead = user.books!.Count();
			}

			return new ResultsSuccessful<User>(user);
		}

		protected override Results<User> addBridges(User user)
		{
			var books = getMultipleJoins<Book, User_Book>(
				x => x.user_id == user.user_id, y => y.book_id);
			var reviews = getJoins<Review>(x => x.user_id == user.user_id);

			if (books.success && reviews.success)
			{
				user.books = books.payload;
				user.reviews = reviews.payload;
				return new ResultsSuccessful<User>(null);
			}
			else
			{
				return new ResultsFailure<User>(
					books.msg
					+ reviews.msg
					+ "Failed to delete joins");
			}
		}

		protected override Results<User> deleteBridges(int id)
		{
			var books = deleteJoins<User_Book>(x => x.user_id == id);
			var reviews = deleteJoins<Review>(x => x.user_id == id);

			if (books.success && reviews.success)
			{
				return new ResultsSuccessful<User>(null);
			}
			else
			{
				return new ResultsFailure<User>(
					books.msg
					+ reviews.msg
					+ "Failed to delete joins");
			}
		}

		protected override User transferProperties(User original, User updated)
		{
			original.pagesRead = updated.pagesRead;
			original.booksRead = updated.booksRead;
			original.books = updated.books;
			original.reviews = updated.reviews;

			return original;
		}

		protected override Results<User> validateProperties(User model)
			=> new ResultsSuccessful<User>(model);
	}
}
