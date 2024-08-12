using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	// I am not going to apply tests for this as this is the same as the
	// IUserRepository interface.
	public interface IBookRepository
	{
		public void addBook(BookModel book);
		public void removeBook(BookModel book);
		public void updateBook(int id, BookModel book);
		public BookModel getBookByID(int id);
		public BookModel getBookByName(String name);
		public BookModel getBookByISBN(String isbn);
		public IEnumerable<BookModel> getAllBooks();
		public IEnumerable<BookModel> getAllBooksOfReader(int userId);
	}
}
