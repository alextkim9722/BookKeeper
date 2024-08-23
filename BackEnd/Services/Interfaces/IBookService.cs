using BackEnd.ErrorHandling;
using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
    // Builds the bookview model using calculations, authors, etc.
    public interface IBookService
    {
        public Results<Book> AddBook(Book book);
        public Results<IEnumerable<Book>> RemoveBook(IEnumerable<int> id);
        public Results<Book> UpdateBook(int id, Book book);
        public Results<Book> GetBookById(int id);
        public Results<Book> GetBookByIsbn(string isbn);
        public Results<IEnumerable<Book>> GetBookByTitle(string title);
    }
}
