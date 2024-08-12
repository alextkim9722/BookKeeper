using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;

namespace MainProject.Datastore
{
	public class UserBookRepository : IUserBookRepository
	{
		private readonly BookShelfContext _context;

		public UserBookRepository(BookShelfContext context)
			=> _context = context;

		public IEnumerable<UserBookModel> getBookIdByUserId(int id)
			=> _context.User_Book.Where(x => x.book_id == id).ToList();

		public IEnumerable<UserBookModel> getUserIdByBookId(int id)
			=> _context.User_Book.Where(x => x.user_id == id).ToList();
	}
}
