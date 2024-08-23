using BackEnd.ErrorHandling;
using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using BackEnd.Services.Generics.Interfaces;
using Microsoft.Data.SqlClient;

namespace BackEnd.Services
{
	public class BookService : IBookService
	{
		private readonly IGenericService<Book> _genericService;
		private readonly IJunctionService<Book_Author> _jBookAuthorService;
		private readonly IJunctionService<Book_Genre> _jBookGenreService;
		private readonly IJunctionService<User_Book> _jUserBookService;
		private readonly IJunctionService<Review> _jReviewService;

		public BookService(
			IGenericService<Book> genericService,
			IJunctionService<Book_Author> jBookAuthorService,
			IJunctionService<Book_Genre> jBookGenreService,
			IJunctionService<User_Book> jUserBookService,
			IJunctionService<Review> jReviewService)
		{
			_genericService = genericService;
			_jBookAuthorService = jBookAuthorService;
			_jBookGenreService = jBookGenreService;
			_jUserBookService = jUserBookService;
			_jReviewService = jReviewService;
		}

		public Results<Book> AddBook(Book book)
			=> _genericService.AddModel(book);

		public Results<IEnumerable<Book>> RemoveBook(IEnumerable<int> id)
		{
			var results = _genericService.DeleteModels(id, deleteDependents);
			return results;
        }

		public Results<Book> UpdateBook(int id, Book book)
			=> _genericService.UpdateModel(x => x.pKey == id, book);

		public Results<Book> GetBookById(int id)
			=> _genericService.ProcessUniqueModel(x => x.pKey == id, AddingProcess);

        public Results<Book> GetBookByIsbn(string isbn)
            => _genericService.ProcessUniqueModel(x => x.isbn == isbn, AddingProcess);

        public Results<IEnumerable<Book>> GetBookByTitle(string title)
			=> _genericService.ProcessModels(x => x.title == title, AddingProcess);

        private Results<Book> AddingProcess(Book book)
		{
			try
			{
				book.authors = _jBookAuthorService.GetJunctionedJoinedModelsId(book.pKey, true);
				book.genres = _jBookGenreService.GetJunctionedJoinedModelsId(book.pKey, true);
				book.users = _jUserBookService.GetJunctionedJoinedModelsId(book.pKey, false);
				book.reviews = _jReviewService.GetJunctionedJoinedModelsId(book.pKey, false);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<Book>("Failed to add bridges");
			}

			setRating(book);

			return new ResultsSuccessful<Book>(book);
		}

		private void setRating(Book book)
		{
			if(!book.reviews.IsNullOrEmpty())
			{
                book.rating = Convert.ToInt32(_jReviewService.GetJunctionModels(book.pKey, false).Average(x => x.rating));
            }
		}

		private Results<Book> deleteDependents(Book book)
		{
            try
            {
                _jBookAuthorService.DeleteJunctionModels(book.pKey, true);
				_jBookGenreService.DeleteJunctionModels(book.pKey, true);
				_jUserBookService.DeleteJunctionModels(book.pKey, false);
				_jReviewService.DeleteJunctionModels(book.pKey, false);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<Book>("Failed to add bridges");
			}

            return new ResultsSuccessful<Book>(book);
        }
	}
}
