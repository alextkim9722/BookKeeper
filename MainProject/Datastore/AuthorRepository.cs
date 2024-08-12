using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Datastore
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly BookShelfContext _context;

		public AuthorRepository(BookShelfContext context)
			=>_context = context;

		public void addAuthor(AuthorModel author)
		{
			_context.Author.Add(author);
			_context.SaveChanges();
		}

		public IEnumerable<AuthorModel> getAllAuthors()
			=> _context.Author.ToList();

		public AuthorModel getAuthorByFirstName(string firstName)
			=> _context.Author.Where(x => x.first_name == firstName).FirstOrDefault();

		public AuthorModel getAuthorById(int id)
			=> _context.Author.Find(id);

		public AuthorModel getAuthorByLastName(string lastName)
			=> _context.Author.Where(x => x.last_name == lastName).FirstOrDefault();

		public void removeAuthor(AuthorModel author)
		{
			_context.Author.Remove(author);
			_context.SaveChanges();
		}

		public void updateAuthor(int id, AuthorModel author)
		{
			if (id != author.author_id) return;

			var author_target = _context.Author.Find(id);
			if (author_target == null) return;

			author_target.first_name = author.first_name;
			author_target.middle_name = author.middle_name;
			author_target.last_name = author.last_name;
			_context.SaveChanges();
		}
	}
}
