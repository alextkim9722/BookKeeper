using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using BackEnd.Services.Generics.Interfaces;
using Microsoft.Data.SqlClient;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services
{
	public class AuthorService : IAuthorService
	{
		private readonly IGenericService<Author> _genericService;
		private readonly IJunctionService<Book_Author> _jBookAuthorService;

		public AuthorService(IGenericService<Author> genericService,
			IJunctionService<Book_Author> jBookAuthorService)
		{
			_genericService = genericService;
			_jBookAuthorService = jBookAuthorService;
		}

		public Results<Author> AddAuthor(Author author)
			=> _genericService.AddModel(author);
		public Results<Author> GetAuthorById(int id)
			=> _genericService.ProcessUniqueModel(x => x.pKey == id);
		public Results<IEnumerable<Author>> GetAuthorByFirstName(string first)
			=> _genericService.ProcessModels(x => x.first_name == first, AddingProcess);
		public Results<IEnumerable<Author>> GetAuthorByLastName(string last)
			=> _genericService.ProcessModels(x => x.last_name == last, AddingProcess);
		public Results<IEnumerable<Author>> GetAuthorByMiddleName(string middle)
			=> _genericService.ProcessModels(x => x.middle_name == middle, AddingProcess);
		public Results<Author> UpdateAuthor(int id, Author author)
			=> _genericService.UpdateModel([id], author);
		public Results<IEnumerable<Author>> RemoveAuthor(IEnumerable<int> id)
			=> _genericService.DeleteModels([id.ToArray()], DeleteDependents);

		private Results<Author> AddingProcess(Author author)
		{
			var depdentsResult = AddDependents(author);
			SetFullName(author);
			return depdentsResult;
		}
		private Results<Author> AddDependents(Author author)
		{
			try
			{
				author.books = _jBookAuthorService.GetJunctionedJoinedModelsId(author.pKey, false);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<Author>("Failed to add bridges");
			}

			return new ResultsSuccessful<Author>(author);
		}

		private void SetFullName(Author author)
		{
			if (author.middle_name.IsNullOrEmpty())
			{
				author.full_name = author.first_name
					+ " " + author.last_name;
			}
			else
			{
				author.full_name = author.first_name
					+ " " + author.middle_name
					+ " " + author.last_name;
			}
		}
		private Results<Author> DeleteDependents(Author author)
		{
			try
			{
				_jBookAuthorService.DeleteJunctionModels(author.pKey, false);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<Author>("Failed to add bridges");
			}

			return new ResultsSuccessful<Author>(author);
		}
	}
}
