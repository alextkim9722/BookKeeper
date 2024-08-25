using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Model;
using BackEnd.Services.Context;
using BackEndTest.Services.DatabaseGenerators.DBConnector;
using BackEndTest.Services.DatabaseGenerators.Interfaces;
using BackEndTest.Services.RandomGenerators;
using Microsoft.EntityFrameworkCore;

namespace BackEndTest.Services.DatabaseGenerators
{
	public class AuthorDatabaseGenerator : IDatabaseGenerator
	{
		private IDatabaseManager databaseManager = new DatabaseManager();
		private ValueGenerators randGen = new ValueGenerators();
		private bool databaseCreated = false;

		public AuthorDatabaseGenerator()
		{
			if(!databaseCreated)
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
		public void PopulateTables(BookShelfContext bookShelfContext)
		{

			var authors = new Author[]
			{
				new Author()
				{
					first_name = "Deborah",
					middle_name = "Nemona",
					last_name = "White"
				},
				new Author()
				{
					first_name = "James",
					last_name = "Remmington"
				},
				new Author()
				{
					first_name = "Blair",
					middle_name = "Witch",
					last_name = "Project"
				}
			};

			bookShelfContext.Author.AddRange(authors);

			for (int i = 0; i < authors.Count(); i++)
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
		public void PopulateBridgeTables(BookShelfContext bookShelfContext)
		{
			var bookAuthors = new Book_Author[]
			{
				new Book_Author()
				{
					firstKey = 3,
					secondKey = 1
				},
				new Book_Author()
				{
					firstKey = 1,
					secondKey = 1
				},
				new Book_Author()
				{
					firstKey = 2,
					secondKey = 2
				},
				new Book_Author()
				{
					firstKey = 2,
					secondKey = 3
				}
			};

			bookShelfContext.Book_Author.AddRange(bookAuthors);
			bookShelfContext.SaveChanges();
		}
	}
}
