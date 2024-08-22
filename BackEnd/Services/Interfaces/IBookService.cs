using BackEnd.ErrorHandling;
using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
    // Builds the bookview model using calculations, authors, etc.
    public interface IBookService
    {
        public Results<Book> addBook(Book book);
        public Results<Book> removeBook(int id);
        public Results<Book> updateBook(int id, Book book);
        public Results<Book> getBookById(int id);
        public Results<Book> getBookByIsbn(string isbn);
        public Results<Book> getBookByTitle(string title);
        public Results<IEnumerable<Book>> getAllBooks();
    }
}
