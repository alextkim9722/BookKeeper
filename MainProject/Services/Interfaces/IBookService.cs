using MainProject.Model;
using MainProject.ViewModel;

namespace MainProject.Services.Interfaces
{
    // Builds the bookview model using calculations, authors, etc.
    public interface IBookService
    {
		public IEnumerable<BookModel> getBookModelFromUser(int id);

        public void addBook(BookModel book);
        public bool removeBook(BookModel book);
        public BookModel updateBook(int id, BookModel book);
        public BookModel? getBook(string field, string value);
        public IEnumerable<BookModel> getAllBooks();
    }
}
