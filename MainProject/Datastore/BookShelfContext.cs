using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

using MainProject.Model;

namespace MainProject.Datastore
{
	public class BookShelfContext : DbContext
	{
		public DbSet<UserModel> users => Set<UserModel>();
	}
}
