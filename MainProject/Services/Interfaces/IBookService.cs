using MainProject.Model;
using MainProject.ViewModel;

namespace MainProject.Services.Interfaces
{
    // Builds the bookview model using calculations, authors, etc.
    public interface IBookService
    {
        public BookModel createBookModel(int id, IEnumerable<AuthorModel> authors, IEnumerable<GenreModel> genres);
        public void formatBookModel(BookModel bookModel, IEnumerable<AuthorModel> authors, IEnumerable<GenreModel> genres);

		public IEnumerable<BookModel> getBookModelFromUser(int id);
    }
}
