using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
	public interface IAuthorService
	{
		public Author? addAuthor(Author author);
		public Author? removeAuthor(int id);
		public Author? updateAuthor(int id, Author author);
		public Author? getAuthorById(int id);
		public Author? getAuthorByFirstName(string first);
		public Author? getAuthorByMiddleName(string middle);
		public Author? getAuthorByLastName(string last);
		public IEnumerable<Author>? getAllAuthors();
	}
}
