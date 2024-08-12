using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;

namespace MainProject.Datastore
{
    public class BookGenreRepository : IBookGenreRepository
    {
        private readonly BookShelfContext _context;

        public BookGenreRepository(BookShelfContext context)
            => _context = context;

        public IEnumerable<BookGenreModel> getBookIdByGenreId(int id)
            => _context.Book_Genre.Where(x => x.book_id == id);

        public IEnumerable<BookGenreModel> getGenreIdByBookId(int id)
            => _context.Book_Genre.Where(x => x.genre_id == id);
    }
}
