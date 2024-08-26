using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services.Interfaces
{
	// Builds the bookview model using calculations, authors, etc.
	public interface IBookService
	{
		public Results<Book> AddBook(Book book);
		public Results<Book> UpdateBook(int id, Book book);
		public Results<Book> GetBookById(int id);
		public Results<Book> GetBookByIsbn(string isbn);
		public Results<IEnumerable<Book>> GetBookByTitle(string title);
		public Results<IEnumerable<Book>> GetBooksByUser(int userId);
		public Results<IEnumerable<Book>> RemoveBooks(IEnumerable<int> id);
		public Results<IEnumerable<Review>> SetRating(Book book, IEnumerable<Review> review);
	}
}
