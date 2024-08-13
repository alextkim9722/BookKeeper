using MainProject.Datastore;
using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.Services.Interfaces;
using MainProject.ViewModel;
using Microsoft.IdentityModel.Tokens;

namespace MainProject.Services
{
	public class AuthorService : IAuthorService
	{
		private readonly IAuthorRepository _authorRepository;

		public AuthorService(IAuthorRepository authorRepository)
			=> _authorRepository = authorRepository;

		public AuthorModel createAuthorModel(int id)
		{
			AuthorModel authorModel = _authorRepository.getAuthorById(id);
			authorModel.full_name = createFullName(authorModel);
			return authorModel;
		}

		// We can't use createAuthorViewModel as it requires an id input
		public void createAuthorModelBatch(IEnumerable<AuthorModel> authorModelList)
			=> authorModelList.ToList().ForEach(x => x.full_name = createFullName(x));

		public IEnumerable<AuthorModel> createAuthorModelBatch(int bookId)
		{
			IEnumerable<AuthorModel> authors = _authorRepository.getAuthorByBook(bookId);
			createAuthorModelBatch(authors);

			return authors;
		}

		// If the middle name doesn't exist, just don't print it
		private string createFullName(AuthorModel author)
		{
			if(author.middle_name.IsNullOrEmpty())
			{
				return author.first_name + " " + author.last_name;
			}
			else
			{
				return author.first_name + " " + author.middle_name + " " + author.last_name;
			}
		}
	}
}
