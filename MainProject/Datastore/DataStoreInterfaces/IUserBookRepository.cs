using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IUserBookRepository
	{
		public IEnumerable<UserBookModel> getUserIdByBookId(int id);
		public IEnumerable<UserBookModel> getBookIdByUserId(int id);
	}
}
