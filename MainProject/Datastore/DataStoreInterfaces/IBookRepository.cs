using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	// I am not going to apply tests for this as this is the same as the
	// IUserRepository interface.
	public interface IBookRepository
	{
		public void addBook(Book book);
		public void removeBook(Book book);
		public Book updateBook(int id, Book book);
		public Book getBookByID(int id);
		public Book getBookByName(String name);
		public Book getBookByISBN(String isbn);
		public IEnumerable<Book> getAllBooks();
		public IEnumerable<Book> getAllBooksOfReader(int userId);
	}
}
