using BackEnd.ErrorHandling;
using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
    public interface IGenreService
	{
		public Results<Genre> addGenre(Genre genre);
		public Results<Genre> removeGenre(int id);
		public Results<Genre> updateGenre(int id, Genre genre);
		public Results<Genre> getGenreById(int id);
		public Results<Genre> getGenreByName(string name);
		public Results<IEnumerable<Genre>> getAllGenres();
	}
}
