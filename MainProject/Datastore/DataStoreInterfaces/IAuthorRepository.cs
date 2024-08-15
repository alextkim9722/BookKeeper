using MainProject.Model;

namespace MainProject.Datastore.DataStoreInterfaces
{
	public interface IAuthorRepository
	{
		public void addAuthor(Author author);
		public void removeAuthor(Author author);
		public void updateAuthor(int id, Author author);
		public Author getAuthorById(int id);
		public Author getAuthorByFirstName(String firstName);
		public Author getAuthorByLastName(String lastName);
		public IEnumerable<Author> getAuthorByBook(int bookId);
		public IEnumerable<Author> getAllAuthors();
	}
}
