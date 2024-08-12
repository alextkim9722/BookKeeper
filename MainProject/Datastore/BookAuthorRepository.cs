using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;

namespace MainProject.Datastore
{
	public class BookAuthorRepository : IBookAuthorRepository
	{
		private readonly BookShelfContext _context;

		public BookAuthorRepository(BookShelfContext context)
			=> _context = context;

		public IEnumerable<BookAuthorModel> getAuthorIdByBookId(int id)
			=> _context.Book_Author.Where(x => x.book_id == id).ToList();

		public IEnumerable<BookAuthorModel> getBookIdByAuthorId(int id)
			=> _context.Book_Author.Where(x => x.author_id == id).ToList();
	}
}
