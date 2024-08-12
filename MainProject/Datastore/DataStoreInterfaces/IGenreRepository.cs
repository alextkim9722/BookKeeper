using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IGenreRepository
	{
		public void addGenre(GenreModel genre);
		public void removeGenre(GenreModel genre);
		public void updateGenre(int id, GenreModel genre);
		public GenreModel getGenreById(int id);
		public GenreModel getGenreByName(String name);
		public IEnumerable<GenreModel> getGenreByBook(int book_id);
		public IEnumerable<GenreModel> getAllGenres();
	}
}
