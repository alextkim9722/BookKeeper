using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
	public interface IGenreService
	{
		public Genre? addGenre(Genre genre);
		public Genre? removeGenre(int id);
		public Genre? updateGenre(int id, Genre genre);
		public Genre? getGenreById(int id);
		public Genre? getGenreByName(string name);
		public IEnumerable<Genre>? getAllGenres();
	}
}
