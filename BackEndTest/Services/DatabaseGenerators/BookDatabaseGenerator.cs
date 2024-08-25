using BackEnd.Model;
using BackEnd.Services.Context;
using BackEndTest.Services.DatabaseGenerators.DBConnector;
using BackEndTest.Services.DatabaseGenerators.Interfaces;
using BackEndTest.Services.RandomGenerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.DatabaseGenerators
{
	public class BookDatabaseGenerator : IDatabaseGenerator
	{
		private IDatabaseManager databaseManager = new DatabaseManager();
		private ValueGenerators randGen = new ValueGenerators();
		private bool databaseCreated = false;

		public BookDatabaseGenerator()
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
		public void PopulateTables(BookShelfContext bookShelfContext)
		{
			var books = new Book[]
			{
				new Book
				{
					title = "Red Book",
					pages = 120,
					isbn = "1ajfespdntle3",
					cover_picture = "red path"
				},
				new Book
				{
					title = "Green Book",
					pages = 30,
					isbn = "1ajfelauivge3",
					cover_picture = "green path"
				},
				new Book
				{
					title = "Blue Book",
					pages = 560,
					isbn = "aifldjsdntle3",
					cover_picture = "blue path"
				},
			};

			bookShelfContext.Book.AddRange(books);

			for(int i = 0;i < books.Count();i++)
			{
				Identification identification = new Identification()
				{
					EmailConfirmed = true,
					LockoutEnabled = false,
					TwoFactorEnabled = false,
					Email = randGen.randString(100) + "@" + "asdfmail.com",
					UserName = randGen.randString(250)
				};
				Genre genre = new Genre()
				{
					genre_name = randGen.randString(10)
				};
				Author author = new Author()
				{
					first_name = randGen.randString(25),
					middle_name = randGen.randString(25),
					last_name = randGen.randString(25)
				};
				User user = new User()
				{
					username = randGen.randString(10),
					identification_id = identification.Id,
					date_joined = randGen.randDate(),
					description = randGen.randString(25),
					profile_picture = randGen.randString(25)
				};

				bookShelfContext.Users.Add(identification);
				bookShelfContext.Genre.Add(genre);
				bookShelfContext.Author.Add(author);
				bookShelfContext.User.Add(user);
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
					secondKey = 1
				}
			};

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

			var userBooks = new User_Book[]
			{
				new User_Book()
				{
					firstKey = 3,
					secondKey = 1
				},
				new User_Book()
				{
					firstKey = 2,
					secondKey = 1
				},
				new User_Book()
				{
					firstKey = 1,
					secondKey = 2
				},
				new User_Book()
				{
					firstKey = 1,
					secondKey = 1
				}
			};

			bookShelfContext.Book_Author.AddRange(bookAuthors);
			bookShelfContext.Book_Genre.AddRange(bookGenres);
			bookShelfContext.User_Book.AddRange(userBooks);

			for (var i = 0;i < userBooks.Count();i++)
			{
				var review = new Review()
				{
					firstKey = userBooks[i].firstKey,
					secondKey = userBooks[i].secondKey,
					date_submitted = randGen.randDate(),
					description = randGen.randString(200),
					rating = 2 * i
				};

				bookShelfContext.Review.Add(review);
			}

			bookShelfContext.SaveChanges();
		}
	}
}
