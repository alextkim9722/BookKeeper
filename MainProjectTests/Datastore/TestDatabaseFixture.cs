using Microsoft.EntityFrameworkCore;
using System;

using MainProject.Datastore;
using MainProject.Model;

namespace MainProjectTests.Datastore
{
	public class TestDatabaseFixture
	{
		private const string connectionString = @"Server=DESKTOP-550OG8P\\MSSQLSERVER2022;Database=BookKeeperDB_Test;Trusted_Connection=True";

		private static readonly object _lock = new();
		private static bool _dataBaseInitialized;

		public TestDatabaseFixture()
		{
			lock (_lock)
			{
				if (!_dataBaseInitialized)
				{
					using (var context = createContext())
					{
						context.Database.EnsureDeleted();
						context.Database.EnsureCreated();

						context.AddRange(
							new UserModel { name = "Alberta123", description = "My name is Alberta and I like 123.", dateJoined = new DateOnly(2015, 12, 13) },
							new UserModel { name = "MarcusF29", description = "From the series known as Gears of War", dateJoined = new DateOnly(2022, 5, 23) }
							);
						context.SaveChanges();
					}

					_dataBaseInitialized = true;
				}
			}
		}
		public BookShelfContext createContext()
		=> new BookShelfContext(
			new DbContextOptionsBuilder<BookShelfContext>()
			.UseSqlServer(connectionString)
			.Options);
	}
}
