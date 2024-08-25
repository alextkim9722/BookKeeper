using BackEnd.Services.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.DatabaseGenerators.DBConnector
{
	public class DatabaseManager : IDatabaseManager
	{
		private const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BookKeeperDB_Test;Data Source=DESKTOP-3B4EF2C;TrustServerCertificate=True;MultipleActiveResultSets=True;";

		public BookShelfContext CreateContext()
			=> new BookShelfContext(
				new DbContextOptionsBuilder<BookShelfContext>()
				.UseSqlServer(connectionString)
				.Options);

		public void ClearTables(BookShelfContext bookShelfContext)
		{
			bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[book_author]");
			bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[book_genre]");
			bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[user_book]");
			bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[review]");
			bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[AspNetUserRoles]");

			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[user]");
			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[book]");
			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[genre]");
			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[author]");
			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[AspNetUsers]");
			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[AspNetRoles]");
		}

		public void ReseedTables(BookShelfContext bookShelfContext)
		{
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('author', RESEED, 0)");
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('book', RESEED, 0)");
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('user', RESEED, 0)");
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('genre', RESEED, 0)");
		}
	}
}
