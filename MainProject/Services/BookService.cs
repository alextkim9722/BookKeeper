using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.Services.Interfaces;

namespace MainProject.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
            => _bookRepository = bookRepository;

		public BookModel createBookModel(int id, IEnumerable<AuthorModel> authors, IEnumerable<GenreModel> genres)
        {
            BookModel bookModel = _bookRepository.getBookByID(id);
            bookModel.authors = authors;
            bookModel.genres = genres;
            return bookModel;
        }

		public void formatBookModel(BookModel bookModel, IEnumerable<AuthorModel> authors, IEnumerable<GenreModel> genres)
		{
			bookModel.authors = authors;
			bookModel.genres = genres;
		}

		public IEnumerable<BookModel> getBookModelFromUser(int id)
            => _bookRepository.getAllBooksOfReader(id);
    }
}
