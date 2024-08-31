using BackEnd.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BackEnd.Services.Context;
using Microsoft.Extensions.DependencyInjection;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
namespace BackEndTest.Services.RandomGenerators
{
	public class TestDatabaseGenerator
	{
		private const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BookKeeperDB_Test;Data Source=DESKTOP-HEF1GNV;TrustServerCertificate=True;MultipleActiveResultSets=True;";
		private bool databaseCreated = false;
		public int _tableValueCount = 5;
		public  int _bridgeTableValueCount = 7;
		private ValueGenerators randGen = new ValueGenerators();

		public List<Author> authorTable { get; set; } = new List<Author>();
		public List<Book> bookTable { get; set; } = new List<Book>();
		public List<Book_Author> bookAuthorTable { get; set; } = new List<Book_Author>();
		public List<Book_Genre> bookGenreTable { get; set; } = new List<Book_Genre>();
		public List<Genre> genreTable { get; set; } = new List<Genre>();
		public List<Review> reviewTable { get; set; } = new List<Review>();
		public List<User> userTable { get; set; } = new List<User>();
		public List<User_Book> userBookTable { get; set; } = new List<User_Book>();
		public List<IdentityRole> roleTable { get; set; } = new List<IdentityRole>();
		public List<Identification> identificationTable { get; set; } = new List<Identification>();

		public TestDatabaseGenerator()
		{
			if (!databaseCreated)
			{
				regenerateTable(createContext());
				databaseCreated = true;
			}
		}

		public BookShelfContext createContext()
		=> new BookShelfContext(
			new DbContextOptionsBuilder<BookShelfContext>()
			.UseSqlServer(connectionString)
			.Options);

		private void regenerateTable(BookShelfContext bookShelfContext)
		{
			clearTables(bookShelfContext);
			reseedTable(bookShelfContext);
			populateTables(bookShelfContext);
			populateBridgeTables(bookShelfContext);
		}

		private void clearTables(BookShelfContext bookShelfContext)
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

		private void reseedTable(BookShelfContext bookShelfContext)
		{
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('author', RESEED, 0)");
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('book', RESEED, 0)");
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('user', RESEED, 0)");
			bookShelfContext.Database.ExecuteSql($"DBCC CHECKIDENT ('genre', RESEED, 0)");
		}

		private void populateTables(BookShelfContext bookShelfContext)
		{
			var manager = new UserManager<Identification>(new UserStore<Identification>(bookShelfContext), null, new PasswordHasher<Identification>(), null, null, null, null, null, null);

			for (int i = 0; i < 10; i++)
			{
				Identification identification = new Identification()
				{
					Id = randGen.randString(200),
					Email = $"[{i + 1}]_Person@tmail.com",
					UserName = $"{i + 1}_Man"
				};
				User user = new User()
				{
					username = identification.UserName,
					identification_id = identification.Id,
					date_joined = randGen.randDate(),
					description = $"{i + 1}User + {randGen.randString(200)}",
					profile_picture = $"/src/assets/{i + 1}ProfilePic.png"
				};

				Task.Run(() => manager.CreateAsync(identification, $"Hello{identification.UserName}!")).GetAwaiter().GetResult();
				bookShelfContext.User.Add(user);
				bookShelfContext.SaveChanges();
			}

			for (int i = 0; i < 100; i++)
			{
				Author author = new Author()
				{
					first_name = $"{i + 1}first",
					middle_name = $"{i + 1}middle",
					last_name = $"{i + 1}last",
				};

				Book book = new Book()
				{
					title = $"{i + 1}Book",
					pages = randGen.randNumber(1, 1000),
					isbn = $"{i + 1}BookIsbn",
					cover_picture = $"/src/assets/{i + 1}BookCover.png"
				};

				Genre genre = new Genre()
				{
					genre_name = $"{i + 1}genre"
				};

				bookShelfContext.Author.Add(author);
				bookShelfContext.Book.Add(book);
				bookShelfContext.Genre.Add(genre);
				bookShelfContext.SaveChanges();
			}
		}

		private void populateBridgeTables(BookShelfContext bookShelfContext)
		{
			var bookAuthorPairGen = new UniqueIntPairGenerator(randGen, 100, 100);
			var bookGenrePairGen = new UniqueIntPairGenerator(randGen, 100, 100);
			var userBookPairGen = new UniqueIntPairGenerator(randGen, 10, 100);

			for (int i = 1; i < 500; i++)
			{
				var bookAuthorPair = bookAuthorPairGen.getRandomPair();
				var bookGenrePair = bookGenrePairGen.getRandomPair();
				var userBookPair = userBookPairGen.getRandomPair();

				Book_Author bookAuthor = new Book_Author()
				{
					firstKey = bookAuthorPair[0],
					secondKey = bookAuthorPair[1]
				};

				Book_Genre bookGenre = new Book_Genre()
				{
					firstKey = bookGenrePair[0],
					secondKey = bookGenrePair[1]
				};

				User_Book userBook = new User_Book()
				{
					firstKey = userBookPair[0],
					secondKey = userBookPair[1]
				};

				Review review = new Review()
				{
					firstKey = userBook.firstKey,
					secondKey = userBook.secondKey,
					description = $"{userBook.firstKey} : {userBook.secondKey} : {randGen.randString(200)}",
					rating = randGen.randNumber(0,10),
					date_submitted = randGen.randDate()
				};

				bookShelfContext.Book_Author.Add(bookAuthor);
				bookShelfContext.Book_Genre.Add(bookGenre);
				bookShelfContext.User_Book.Add(userBook);
				bookShelfContext.Review.Add(review);
				bookShelfContext.SaveChanges();
			}
		}
	}
}
