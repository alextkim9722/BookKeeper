using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IBookGenreRepository
	{
		public IEnumerable<BookGenreModel> getGenreIdByBookId(int id);
		public IEnumerable<BookGenreModel> getBookIdByGenreId(int id);
	}
}
