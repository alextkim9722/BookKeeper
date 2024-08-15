using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MainProject.Datastore
{
	public class GenreRepository : IGenreRepository
	{
		private readonly BookShelfContext _context;

		public GenreRepository(BookShelfContext context)
			=>_context = context;

		public void addGenre(Genre genre)
		{
			_context.Genre.Add(genre);
			_context.SaveChanges();
		}

		public IEnumerable<Genre> getAllGenres()
			=> _context.Genre.ToList();

		public IEnumerable<Genre> getGenreByBook(int bookId)
		{
			var genre_bridge
				= _context.Book_Genre
				.Where(x => x.book_id == bookId)
				.Select(x => x.genre_id)
				.ToList();
			return _context.Genre.Where(x => genre_bridge.Contains(x.genre_id)).ToList();
		}

		public Genre getGenreById(int id)
			=> _context.Genre.Find(id);

		public Genre getGenreByName(string name)
			=> _context.Genre.Where(x => x.genre_name == name).FirstOrDefault();

		public void removeGenre(Genre genre)
		{
			_context.Genre.Remove(genre);
			_context.SaveChanges();
		}

		public void updateGenre(int id, Genre genre)
		{
			if (id != genre.genre_id) return;

			var genre_target = _context.Book.Find(id);
			if (genre_target == null) return;

			genre_target.title = genre.genre_name;
			_context.SaveChanges();
		}
	}
}
