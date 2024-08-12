using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IAuthorRepository
	{
		public void addAuthor(AuthorModel author);
		public void removeAuthor(AuthorModel author);
		public void updateAuthor(int id, AuthorModel author);
		public AuthorModel getAuthorById(int id);
		public AuthorModel getAuthorByFirstName(String firstName);
		public AuthorModel getAuthorByLastName(String lastName);
		public IEnumerable<AuthorModel> getAuthorByBook(int bookId);
		public IEnumerable<AuthorModel> getAllAuthors();
	}
}
