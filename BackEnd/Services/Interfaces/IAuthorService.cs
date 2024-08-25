using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services.Interfaces
{
	public interface IAuthorService
	{
		public Results<Author> AddAuthor(Author author);
		public Results<Author> GetAuthorById(int id);
		public Results<IEnumerable<Author>> GetAuthorByFirstName(string first);
		public Results<IEnumerable<Author>> GetAuthorByLastName(string last);
		public Results<IEnumerable<Author>> GetAuthorByMiddleName(string middle);
		public Results<Author> UpdateAuthor(int id, Author author);
		public Results<IEnumerable<Author>> RemoveAuthor(IEnumerable<int> id);
	}
}
