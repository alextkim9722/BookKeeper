using BackEnd.Services.Context;
using BackEnd.Model;
using BackEndTest.Services.DatabaseGenerators.DBConnector;
using BackEndTest.Services.DatabaseGenerators.Interfaces;
using BackEndTest.Services.RandomGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.DatabaseGenerators
{
	public class GenreDatabaseGenerator : IDatabaseGenerator
	{
		private IDatabaseManager databaseManager = new DatabaseManager();
		private ValueGenerators randGen = new ValueGenerators();
		private bool databaseCreated = false;

		public GenreDatabaseGenerator()
		{
			if (!databaseCreated)
			{
				var context = databaseManager.CreateContext();
				databaseManager.ClearTables(context);
				databaseManager.ReseedTables(context);
				PopulateTables(context);
				PopulateBridgeTables(context);
				databaseCreated = true;
			}
		}

		public BookShelfContext GetContext() => databaseManager.CreateContext();

		public void PopulateBridgeTables(BookShelfContext bookShelfContext)
		{
			var bookGenres = new Book_Genre[]
			{
				new Book_Genre()
				{
					firstKey = 2,
					secondKey = 1
				},
				new Book_Genre()
				{
					firstKey = 3,
					secondKey = 1
				},
				new Book_Genre()
				{
					firstKey = 3,
					secondKey = 2
				},
				new Book_Genre()
				{
					firstKey = 1,
					secondKey = 1
				}
			};

			bookShelfContext.Book_Genre.AddRange(bookGenres);
			bookShelfContext.SaveChanges();
		}

		public void PopulateTables(BookShelfContext bookShelfContext)
		{
			var genres = new Genre[]
			{
				new Genre()
				{
					genre_name = "scifi"
				},
				new Genre()
				{
					genre_name = "fantasy"
				},
				new Genre()
				{
					genre_name = "fiction"
				}
			};

			bookShelfContext.Genre.AddRange(genres);

			for(var i = 0;i < genres.Length;i++)
			{
				Book book = new Book()
				{
					title = randGen.randString(25),
					pages = randGen.randNumber(1, 1000),
					isbn = randGen.randString(13),
					cover_picture = randGen.randString(25)
				};

				bookShelfContext.Book.Add(book);
			}

			bookShelfContext.SaveChanges();
		}
	}
}
