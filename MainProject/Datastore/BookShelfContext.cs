using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

using MainProject.Model;

namespace MainProject.Datastore
{
	public class BookShelfContext : DbContext
	{
		public BookShelfContext(DbContextOptions<BookShelfContext> options) : base(options) {}

		public DbSet<UserModel> User => Set<UserModel>();
	}
}
