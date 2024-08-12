using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IBookAuthorRepository
	{
		public IEnumerable<BookAuthorModel> getAuthorIdByBookId(int id);
		public IEnumerable<BookAuthorModel> getBookIdByAuthorId(int id);
	}
}
