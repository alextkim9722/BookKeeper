using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System;

using MainProject.Datastore;
using MainProject.Model;

namespace MainProjectTest.Datastore
{
	public class TestDatabaseFixture
	{
		private const string connectionString = "Server=DESKTOP-550OG8P\\MSSQLSERVER2022;Database=BookKeeperDB_Test;Trusted_Connection=True;TrustServerCertificate=True";

		public TestDatabaseFixture()
		{
			// Empty
		}
		public BookShelfContext createContext()
		=> new BookShelfContext(
			new DbContextOptionsBuilder<BookShelfContext>()
			.UseSqlServer(connectionString)
			.Options);
	}
}
