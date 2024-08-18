using BackEnd.Services;
using BackEnd.Model;
using BackEndTest.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
	public class TestDatabaseGenerator
	{
		private const string connectionString = "Server=DESKTOP-550OG8P\\MSSQLSERVER2022;Database=BookKeeperDB_Test;Trusted_Connection=True;TrustServerCertificate=True";
		private static bool databaseCreated = false;

		private RandomGenerators randGen = new RandomGenerators();

		public List<Author> authorTable { get; set; } = new List<Author>();
		public List<Book> bookTable { get; set; } = new List<Book>();
		public List<Book_Author> bookAuthorTable { get; set; } = new List<Book_Author>();
		public List<Book_Genre> bookGenreTable { get; set; } = new List<Book_Genre>();
		public List<Genre> genreTable { get; set; } = new List<Genre>();
		public List<Review> reviewTable { get; set; } = new List<Review>();
		public List<User> userTable { get; set; } = new List<User>();
		public List<User_Book> userBookTable { get; set; } = new List<User_Book>();

		public TestDatabaseGenerator()
		{
			regenerateTable(createContext());
		}

		public void regenerateTable(BookShelfContext bookShelfContext)
		{
			clearTables(bookShelfContext);
			populateTables(bookShelfContext);
			populateBridgeTables(bookShelfContext);
		}

		public BookShelfContext createContext()
		=> new BookShelfContext(
			new DbContextOptionsBuilder<BookShelfContext>()
			.UseSqlServer(connectionString)
			.Options);

		public void clearTables(BookShelfContext bookShelfContext)
		{
			bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[book_author]");
			bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[book_genre]");
			bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[user_book]");
			bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[review]");

			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[user]");
			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[book]");
			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[genre]");
			bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[author]");
		}

		public void populateTables(BookShelfContext bookShelfContext)
		{
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('author', RESEED, 0)");
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('book', RESEED, 0)");
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('user', RESEED, 0)");
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('genre', RESEED, 0)");

			for (int i = 0;i < 20; i++)
			{
				Author author = new Author()
				{
					first_name = randGen.randString(25),
					middle_name = randGen.randString(25),
					last_name = randGen.randString(25),
				};

				Book book = new Book()
				{
					title = randGen.randString(25),
					pages = randGen.randNumber(1, 1000),
					isbn = randGen.randString(25),
					cover_picture = randGen.randString(25)
				};

				User user = new User()
				{
					username = randGen.randString(25),
					identification_id = randGen.randString(25),
					date_joined = randGen.randDate(),
					description = randGen.randString(25),
					profile_picture = randGen.randString(25)
				};

				Genre genre = new Genre()
				{
					genre_name = randGen.randString(25)
				};

				authorTable.Add(author);
				bookTable.Add(book);
				userTable.Add(user);
				genreTable.Add(genre);

				using(var transcation = bookShelfContext.Database.BeginTransaction())
				{
					bookShelfContext.Author.Add(author);
					bookShelfContext.Book.Add(book);
					bookShelfContext.User.Add(user);
					bookShelfContext.Genre.Add(genre);
					bookShelfContext.SaveChanges();
					transcation.Commit();
				}
			}
		}

		private List<int[]> generatePairs(int range)
		{
			List<int[]> pairs = new List<int[]>();

			for (int i = 1; i < range; i++)
			{
				for (int j = 1; j < range; j++)
				{
					pairs.Add(new int[] { i, j });
				}
			}

			return pairs;
		}

		public void populateBridgeTables(BookShelfContext bookShelfContext)
		{
			List<int[]> bkat = generatePairs(20);
			List<int[]> bkgn = generatePairs(20);
			List<int[]> usbk = generatePairs(20);

			for (int i = 1; i < 30; i++)
			{
				var bkatIndex = randGen.randNumber(0, bkat.Count() - 1);
				var bkgnIndex = randGen.randNumber(0, bkgn.Count() - 1);
				var usbkIndex = randGen.randNumber(0, usbk.Count() - 1);

				Book_Author bookAuthor = new Book_Author()
				{
					book_id = bkat.ElementAt(bkatIndex)[0],
					author_id = bkat.ElementAt(bkatIndex)[1]
				};

				Book_Genre bookGenre = new Book_Genre()
				{
					book_id = bkgn.ElementAt(bkgnIndex)[0],
					genre_id = bkgn.ElementAt(bkgnIndex)[1]
				};

				User_Book userBook = new User_Book()
				{
					book_id = usbk.ElementAt(usbkIndex)[0],	
					user_id = usbk.ElementAt(usbkIndex)[1]
				};

				Review review = new Review()
				{
					book_id = userBook.book_id,
					user_id = userBook.user_id,
					description = randGen.randString(25),
					rating = 0,
					date_submitted = randGen.randDate()
				};

				bkat.RemoveAt(bkatIndex);
				bkgn.RemoveAt(bkgnIndex);
				usbk.RemoveAt(usbkIndex);

				bookAuthorTable.Add(bookAuthor);
				bookGenreTable.Add(bookGenre);
				userBookTable.Add(userBook);
				reviewTable.Add(review);

				using (var transcation = bookShelfContext.Database.BeginTransaction())
				{
					bookShelfContext.Book_Author.Add(bookAuthor);
					bookShelfContext.Book_Genre.Add(bookGenre);
					bookShelfContext.User_Book.Add(userBook);
					bookShelfContext.Review.Add(review);
					bookShelfContext.SaveChanges();
					transcation.Commit();
				}
			}
		}
	}
}
