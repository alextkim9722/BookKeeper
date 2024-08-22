using BackEnd.Model;
using BackEnd.Services.Abstracts;
using BackEnd.Services.ErrorHandling;
using BackEnd.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Services
{
    public class BookService : IBookService
	{
		private readonly ITableService<Book> _tableService;

		public BookService(ITableService<Book> tableService)
		{
			_tableService = tableService;
		}

		public Results<Book> addBook(Book book)
			=> addModel(book);

		public Results<Book> removeBook(int id)
			=> deleteBridgedModel(id, x => x.book_id == id);

		public Results<Book> updateBook(int id, Book book)
			=> updateModel(x => x.book_id == id, book);

		public Results<Book> getBookById(int id)
			=> formatModel(x => x.book_id == id);

		public Results<Book> getBookByIsbn(string isbn)
			=> formatModel(x => x.isbn == isbn);

		public Results<Book> getBookByTitle(string title)
			=> formatModel(x => x.title == title);

		public Results<IEnumerable<Book>> getAllBooks()
			=> formatAllModels();

		protected override Results<Book> addBridges(Book book)
		{
			var authors = getMultipleJoins<Author, Book_Author>(
				x => x.book_id == book.book_id, y => y.author_id);
			var genres = getMultipleJoins<Genre, Book_Genre>(
				x => x.book_id == book.book_id, y => y.genre_id);
			var readers = getMultipleJoins<User, User_Book>(
				x => x.book_id == book.book_id, y => y.user_id);
			var reviews = getJoins<Review>(x => x.book_id == book.book_id);

			if(reviews.success && authors.success && genres.success && readers.success)
			{
				book.authors = authors.payload;
				book.genres = genres.payload;
				book.readers = readers.payload!.Count();
				book.reviews = reviews.payload;
				
				if(!book.reviews.IsNullOrEmpty())
				{
					book.rating = Convert.ToInt32(book.reviews!.Select(x => x.rating).Average());
				}
				else
				{
					book.rating = 0;
				}

				return new ResultsSuccessful<Book>(book);
			}
			else
			{
				return new ResultsFailure<Book>(
					authors.msg
					+ genres.msg
					+ readers.msg
					+ reviews.msg
					+ "Failed to grab bridge tables");
			}
		}
		protected override Results<Book> deleteBridges(int id)
		{
			var authors = deleteJoins<Book_Author>(x => x.book_id == id);
			var genres = deleteJoins<Book_Genre>(x => x.book_id == id);
			var readers = deleteJoins<User_Book>(x => x.book_id == id);
			var reviews = deleteJoins<Review>(x => x.book_id == id);

			if (authors.success && genres.success && 
				readers.success && reviews.success)
			{
				return new ResultsSuccessful<Book>(null);
			}else
			{
				return new ResultsFailure<Book>(
					authors.msg
					+ genres.msg
					+ readers.msg
					+ reviews.msg
					+ "Failed to delete joins");
			}
		}

		protected override Book transferProperties(Book original, Book updated)
		{
			original.authors = updated.authors;
			original.genres = updated.genres;
			original.readers = updated.readers;
			original.reviews = updated.reviews;

			return original;
		}

		protected override Results<Book> validateProperties(Book model)
		=> new ResultsSuccessful<Book>(model);
	}
}
