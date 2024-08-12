using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

using MainProject.Model;

namespace MainProject.Datastore
{
	public class BookShelfContext : DbContext
	{
		public BookShelfContext(DbContextOptions<BookShelfContext> options) : base(options) {}

		public DbSet<UserModel> User {  get; set; }
		public DbSet<BookModel> Book { get; set; }
		public DbSet<AuthorModel> Author { get; set; }
		public DbSet<GenreModel> Genre { get; set; }
		public DbSet<BookGenreModel> Book_Genre { get; set; }
		public DbSet<BookAuthorModel> Book_Author { get; set; }
		public DbSet<UserBookModel> User_Book { get; set; }
	}
}
