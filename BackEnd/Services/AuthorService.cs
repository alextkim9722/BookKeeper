using BackEnd.Model;
using BackEnd.Services.Abstracts;
using BackEnd.Services.Interfaces;
using BackEnd.Services.ErrorHandling;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Services
{
    public class AuthorService : JoinServiceAbstract<Author>, IAuthorService
	{
		public AuthorService(BookShelfContext bookShelfContext) :
			base(bookShelfContext)
		{
			CallbackHandler.Add(addBridges);
			CallbackHandler.Add(addFullName);
		}

		// If the middle name doesn't exist, just don't print it
		private Results<Author> addFullName(Author author)
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

			return new ResultsSuccessful<Author>(author);
		}
		protected override Results<Author> addBridges(Author author)
		{
			var result = getMultipleJoins<Book, Book_Author>(
				x => x.author_id == author.author_id,
				y => y.book_id);

			if (result.success)
			{
				author.books = result.payload;
				return new ResultsSuccessful<Author>(author);
			}
			else
			{
				return new ResultsFailure<Author>(
					result.msg
					+ "Failed to grab bridge tables");
			}
		}
		protected override Results<Author> deleteBridges(int id)
		{
			return
				deleteJoins<Book_Author>(x => x.author_id == id);
		}

		protected override Author transferProperties(Author original, Author updated)
		{
			original.books = updated.books;
			return original;
		}

		public Results<Author> addAuthor(Author author)
			=> addModel(author);
		public Results<IEnumerable<Author>> getAllAuthors()
			=> formatAllModels();
		public Results<Author> getAuthorByFirstName(string first)
			=> formatModel(x => x.first_name == first);
		public Results<Author> getAuthorById(int id)
			=> formatModel(x => x.author_id == id);
		public Results<Author> getAuthorByLastName(string last)
			=> formatModel(x => x.last_name == last);
		public Results<Author> getAuthorByMiddleName(string middle)
			=> formatModel(x => x.middle_name == middle);
		public Results<Author> removeAuthor(int id)
			=> deleteBridgedModel(id, x => x.author_id == id);
		public Results<Author> updateAuthor(int id, Author author)
			=> updateModel(x => x.author_id == id, author);

		protected override Results<Author> validateProperties(Author model)
			=> new ResultsSuccessful<Author>(model);
	}
}
