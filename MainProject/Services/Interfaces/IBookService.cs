using MainProject.Model;
using MainProject.ViewModel;

namespace MainProject.Services.Interfaces
{
    // Builds the bookview model using calculations, authors, etc.
    public interface IBookService
    {
        public Book? addBook(Book book);
        public Book? removeBook(int id);
        public Book? updateBook(int id, Book book);
        public Book? getBookById(int id);
        public Book? getBookByIsbn(string isbn);
        public Book? getBookByTitle(string title);
        public IEnumerable<Book>? getAllBooks();
    }
}
