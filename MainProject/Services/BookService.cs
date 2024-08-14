using MainProject.Datastore;
using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Xunit.Sdk;

namespace MainProject.Services
{
    public class BookService : BridgeTableServiceAbstract, IBookService
	{
        public BookService(BookShelfContext bookShelfContext) :
			base(bookShelfContext, "book_id", "book") { }

		public BookModel addAuthorsGenresReaders(BookModel bookModel)
		{
			bookModel.authors = getBridgeTableConnections<AuthorModel>(
				"book_author",
				"author",
				bookModel.book_id,
				"author_id");
			bookModel.genres = getBridgeTableConnections<GenreModel>(
				"book_genre",
				"genre",
				bookModel.book_id,
				"genre_id");
			bookModel.readers = getBridgeTableConnections<UserModel>(
				"user_book",
				"user",
				bookModel.book_id,
				"user_id").Count();

			return bookModel;
		}

		public IEnumerable<BookModel> getBookModelFromUser(int id)
			=> throw new NotImplementedException();


		public void addBook(BookModel book)
		{
			_bookShelfContext.Book.Add(book);
			_bookShelfContext.SaveChanges();
		}
		public bool removeBook(BookModel book)
		{
			if (deleteBridgeTableConnection("book_author", book.book_id) &&
				deleteBridgeTableConnection("book_genre", book.book_id) &&
				deleteBridgeTableConnection("user_book", book.book_id))
			{
				_bookShelfContext.Book.Remove(book);
				_bookShelfContext.SaveChanges();

				return true;
			}
			else
			{
				return false;
			}
		}

		// Returns a Bookmodel so it can be used to update a instanced book if needed.
		public BookModel updateBook(int id, BookModel book)
		{
			if (id != book.book_id) return book;

			var book_target = getBook(_primaryKeyName, id.ToString());
			if (book_target == null) return book;

			book_target.title = book.title;
			book_target.authors = book.authors;
			book_target.isbn = book.isbn;
			book_target.pages = book.pages;
			book_target.book_id = book.book_id;
			book_target.genres = book.genres;

			_bookShelfContext.SaveChanges();

			return book_target;
		}

		public IEnumerable<BookModel> getAllBooks()
		{
			IEnumerable<BookModel> bookModels = _bookShelfContext.Book.ToList();
			foreach (var bookModel in bookModels)
			{
				addAuthorsGenresReaders(bookModel);
			}

			return bookModels;
		}

		public BookModel? getBook(string field, string value)
		{
			BookModel? bookModel = getModelBy<BookModel>(field, value);
			if (bookModel == null) return null;
			else return addAuthorsGenresReaders(bookModel);
		}

		// Exists for testing purposes
		//public IEnumerable<AuthorModel> getAuthors(int bookid)
		//	=> getBridgeTableConnections<AuthorModel>("book_author", "author", bookid, "book_id", "author_id");
	}
}
