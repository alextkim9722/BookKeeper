using BackEnd.ErrorHandling;
using BackEnd.Model;
using BackEnd.Repository.Interfaces;
using BackEnd.Services.Interfaces;
using BackEnd.Services.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Services
{
    public class BookService : AbstractService<Book>, IBookService
	{
		
        private readonly IKeyBatchService<Review> _reviewBatchService;
        private readonly IJunctionService<Author, Book_Author> _junctionAuthorService;
        private readonly IJunctionService<Genre, Book_Genre> _junctionGenreService;
        private readonly IJunctionService<User, User_Book> _junctionUserService;

        public BookService(
			ITableService<Book> tableService,
			IKeyBatchService<Book> keyBatchService,
            IKeyBatchService<Review> reviewBatchService,
			IJunctionService<Author, Book_Author> junctionAuthorService,
			IJunctionService<Genre, Book_Genre> junctionGenreService,
			IJunctionService<User, User_Book> junctionUserService)
			: base(tableService, keyBatchService)
		{
			_reviewBatchService = reviewBatchService;
			_junctionAuthorService = junctionAuthorService;
			_junctionGenreService = junctionGenreService;
			_junctionUserService = junctionUserService;
		}

		public Results<Book> addBook(Book book)
			=> _tableService.addModel(book);

		public Results<Book> removeBook(int id)
			=> deleteBridgedModel(id, x => x.book_id == id);

		public Results<Book> updateBook(int id, Book book)
			=> _tableService.updateModel(x => x.book_id == id, book);

		public Results<Book> getBookById(int id)
			=> _tableService.getUniqueModel(x => x.book_id == id);

		public Results<Book> getBookByIsbn(string isbn)
			=> _tableService.getUniqueModel(x => x.isbn == isbn);

		public Results<IEnumerable<Book>> getBookByTitle(string title)
			=> _keyBatchService.getModels(x => x.title == title);

		public Results<IEnumerable<Book>> getAllBooks()
			=> _tableService.getAllModels();

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
	}
}
