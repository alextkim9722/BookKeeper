using BackEnd.Model;
using BackEnd.Services.Abstracts;
using BackEnd.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace BackEnd.Services
{
    public class AuthorService : JoinServiceAbstract<Author>, IAuthorService
	{
		private readonly Callback handler1;
		private readonly Callback handler2;

		public AuthorService(BookShelfContext bookShelfContext) :
			base(bookShelfContext)
		{
			handler1 = addBridges;
			handler2 = addFullName;
			CallbackHandler = handler1 + handler2;
		}

		public Author? addAuthor(Author author)
			=> addAuthor(author);

		public IEnumerable<Author>? getAllAuthors()
			=> formatAllModels();

		public Author? getAuthorByFirstName(string first)
			=> formatModel(CallbackHandler, null, x => x.first_name == first);

		public Author? getAuthorById(int id)
			=> formatModel(CallbackHandler, null, x => x.author_id == id);

		public Author? getAuthorByLastName(string last)
			=> formatModel(CallbackHandler, null, x => x.last_name == last);

		public Author? getAuthorByMiddleName(string middle)
			=> formatModel(CallbackHandler, null, x => x.middle_name == middle);

		public Author? removeAuthor(int id)
		{
			if (deleteBridges(id))
			{
				return deleteModel(x => x.author_id == id);
			}
			else
			{
				return null;
			}
		}

		public Author? updateAuthor(int id, Author author)
		{
			Author? updatedAuthor = updateModel(id, author);

			if (updatedAuthor != null)
			{
				updatedAuthor.books = author.books;

				return updatedAuthor;
			}
			else
			{
				return null;
			}
		}

		// If the middle name doesn't exist, just don't print it
		private Author? addFullName(Author? author)
		{
			try
			{
				if (author != null)
				{
					if (author.middle_name.IsNullOrEmpty())
					{
						author.full_name = author.first_name
							+ " " + author.last_name;
						return author;
					}
					else
					{
						author.full_name = author.first_name
							+ " " + author.middle_name
							+ " " + author.last_name;
						return author;
					}
				}
				else
				{
					return null;
				}
			}catch(Exception ex) {

				Console.WriteLine(ex.Message);
				return null;
			}
		}

		protected override Author addBridges(Author author)
		{
			author.books = getMultipleJoins<Book, Book_Author>(
				x => x.author_id == author.author_id,
				y => y.book_id);

			return author;
		}
		protected override bool deleteBridges(int id)
		{
			return
				deleteJoins<Book_Author>(x => x.author_id == id);
		}
	}
}
