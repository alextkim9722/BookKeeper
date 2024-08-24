using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services.Interfaces
{
    public interface IGenreService
	{
		public Results<Genre> AddGenre(Genre genre);
		public Results<Genre> GetGenreById(int id);
		public Results<IEnumerable<Genre>> GetGenreByName(string name);
		public Results<Genre> UpdateGenre(int id, Genre genre);
		public Results<IEnumerable<Genre>> RemoveGenre(IEnumerable<int> id);
	}
}
