using BackEnd.ErrorHandling;
using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
    public interface IAuthorService
	{
		public Results<Author> addAuthor(Author author);
		public Results<Author> removeAuthor(int id);
		public Results<Author> updateAuthor(int id, Author author);
		public Results<Author> getAuthorById(int id);
		public Results<Author> getAuthorByFirstName(string first);
		public Results<Author> getAuthorByMiddleName(string middle);
		public Results<Author> getAuthorByLastName(string last);
		public Results<IEnumerable<Author>> getAllAuthors();
	}
}
