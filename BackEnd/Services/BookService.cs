using BackEnd.Model;
using BackEnd.Services.Abstracts;
using BackEnd.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Services
{
    public class BookService : JoinServiceAbstract<Book>, IBookService
	{
		private readonly Callback handler1;

		public BookService(BookShelfContext bookShelfContext) :
			base(bookShelfContext)
		{
			handler1 = addBridges;
			CallbackHandler += handler1;
		}

		public Book? addBook(Book book)
			=> addModel(book);

		public Book? removeBook(int id)
		{
			if (deleteBridges(id))
			{
				return deleteModel(x => x.book_id == id);
			}
			else
			{
				return null;
			}
		}

		public Book? updateBook(int id, Book book)
		{
			Book? updatedBook = updateModel(
				x => x.book_id == id, book);

			if (updatedBook != null)
			{
				updatedBook.authors = book.authors;
				updatedBook.genres = book.genres;
				updatedBook.readers = book.readers;
				updatedBook.reviews = book.reviews;
			}

			return updatedBook;
		}

		public Book? getBookById(int id)
			=> formatModel(null, x => x.book_id == id);

		public Book? getBookByIsbn(string isbn)
			=> formatModel(null, x => x.isbn == isbn);

		public Book? getBookByTitle(string title)
			=> formatModel(null, x => x.title == title);

		public IEnumerable<Book>? getAllBooks()
			=> formatAllModels();

		protected override Book addBridges(Book book)
		{
			book.authors = getMultipleJoins<Author, Book_Author>(
				x => x.book_id == book.book_id, y => y.author_id);
			book.genres = getMultipleJoins<Genre, Book_Genre>(
				x => x.book_id == book.book_id, y => y.genre_id);
			book.reviews = getJoins<Review>(x => x.book_id == book.book_id);

			if(!book.reviews.IsNullOrEmpty())
			{
				book.rating = Convert.ToInt32(book.reviews.Select(x => x.rating).Average());
			}

			var readersModels = getMultipleJoins<User, User_Book>(
				x => x.book_id == book.book_id, y => y.user_id);

			if (readersModels != null)
			{
				book.readers = readersModels.Count();
			}
			else
			{
				book.readers = null;
			}

			return book;
		}
		protected override bool deleteBridges(int id)
		{
			return
				deleteJoins<Book_Author>(x => x.book_id == id) &&
				deleteJoins<Book_Genre>(x => x.book_id == id) &&
				deleteJoins<User_Book>(x => x.book_id == id) &&
				deleteJoins<Review>(x => x.book_id == id);
		}
	}
}
