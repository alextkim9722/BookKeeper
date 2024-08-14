using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MainProject.Datastore
{
	public class BookRepository : IBookRepository
	{
		private readonly BookShelfContext _context;

		public BookRepository(BookShelfContext context)
			=>_context = context;

		public void addBook(BookModel book)
		{
			_context.Book.Add(book);
			_context.SaveChanges();
		}

		public void updateBook(int id, BookModel book)
		{
			if (id != book.book_id) return;

			var book_target = _context.Book.Find(id);
			if (book_target == null) return;

			book_target.title = book.title;
			book_target.pages = book.pages;
			book_target.isbn = book.isbn;
			book_target.rating = book.rating;
			book_target.cover_picture = book.cover_picture;
			_context.SaveChanges();
		}

		public IEnumerable<BookModel> getAllBooks()
			=> _context.Book.ToList();

		public IEnumerable<BookModel> getAllBooksOfReader(int userId)
		{
			var user_bridge
				= _context.User_Book
				.Where(x => x.user_id == userId)
				.Select(x => x.book_id)
				.ToList();
			return _context.Book.Where(x => user_bridge.Contains(x.book_id)).ToList();
		}

		public BookModel getBookByID(int id)
			=> _context.Book.Find(id);

		public BookModel getBookByISBN(string isbn)
			=> _context.Book.Where(x => x.isbn == isbn).FirstOrDefault();

		public BookModel getBookByName(string name)
			=> _context.Book.Where(x => x.title == name).FirstOrDefault();

		public void removeBook(BookModel book)
		{
			_context.Book.Remove(book);
			_context.SaveChanges();
		}

		BookModel IBookRepository.updateBook(int id, BookModel book)
		{
			throw new NotImplementedException();
		}
	}
}
