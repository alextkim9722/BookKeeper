using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IGenreRepository
	{
		public void addGenre(Genre genre);
		public void removeGenre(Genre genre);
		public void updateGenre(int id, Genre genre);
		public Genre getGenreById(int id);
		public Genre getGenreByName(String name);
		public IEnumerable<Genre> getGenreByBook(int book_id);
		public IEnumerable<Genre> getAllGenres();
	}
}
