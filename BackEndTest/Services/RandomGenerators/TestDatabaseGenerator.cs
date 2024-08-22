using BackEnd.Services;
using BackEnd.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BackEndTest.Services.RandomGenerators
{
    public class TestDatabaseGenerator
    {
        private const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BookKeeperDB_Test;Data Source=DESKTOP-3B4EF2C;TrustServerCertificate=True";
        private static bool databaseCreated = false;
        public static int _tableValueCount = 5;
        public static  int _bridgeTableValueCount = 7;
        private RandomGenerators randGen = new RandomGenerators();

        public static List<Author> authorTable { get; set; } = new List<Author>();
        public static List<Book> bookTable { get; set; } = new List<Book>();
        public static List<Book_Author> bookAuthorTable { get; set; } = new List<Book_Author>();
        public static List<Book_Genre> bookGenreTable { get; set; } = new List<Book_Genre>();
        public static List<Genre> genreTable { get; set; } = new List<Genre>();
        public static List<Review> reviewTable { get; set; } = new List<Review>();
        public static List<User> userTable { get; set; } = new List<User>();
        public static List<User_Book> userBookTable { get; set; } = new List<User_Book>();
		public static List<IdentityRole> roleTable { get; set; } = new List<IdentityRole>();
		public static List<Identification> identificationTable { get; set; } = new List<Identification>();

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
            for (int i = 0; i < _tableValueCount; i++)
            {
                Identification identification = new Identification()
                {
                    Id = randGen.randString(25),
                    AccessFailedCount = 0,
                    Email = randGen.randString(25),
                    UserName = randGen.randString(25)
                };

                IdentityRole identityRole = new IdentityRole()
                {
                    Name = randGen.randString(25)
                };

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
                    identification_id = identification.Id,
                    date_joined = randGen.randDate(),
                    description = randGen.randString(25),
                    profile_picture = randGen.randString(25)
                };

                Genre genre = new Genre()
                {
                    genre_name = randGen.randString(25)
                };

                identificationTable.Add(identification);
                roleTable.Add(identityRole);
                authorTable.Add(author);
                bookTable.Add(book);
                userTable.Add(user);
                genreTable.Add(genre);

                

                using (var transcation = bookShelfContext.Database.BeginTransaction())
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

        private void populateBridgeTables(BookShelfContext bookShelfContext)
        {
            var bookAuthorPairGen = new UniqueIntPairGenerator(randGen, _tableValueCount, _tableValueCount);
            var bookGenrePairGen = new UniqueIntPairGenerator(randGen, _tableValueCount, _tableValueCount);
            var userBookPairGen = new UniqueIntPairGenerator(randGen, _tableValueCount, _tableValueCount);

            for (int i = 1; i < _bridgeTableValueCount; i++)
            {
                var bookAuthorPair = bookAuthorPairGen.getRandomPair();
                var bookGenrePair = bookGenrePairGen.getRandomPair();
                var userBookPair = userBookPairGen.getRandomPair();

                Book_Author bookAuthor = new Book_Author()
                {
                    book_id = bookAuthorPair[0],
                    author_id = bookAuthorPair[1]
                };

                Book_Genre bookGenre = new Book_Genre()
                {
                    book_id = bookGenrePair[0],
                    genre_id = bookGenrePair[1]
                };

                User_Book userBook = new User_Book()
                {
                    book_id = userBookPair[0],
                    user_id = userBookPair[1]
                };

                Review review = new Review()
                {
                    book_id = userBook.book_id,
                    user_id = userBook.user_id,
                    description = randGen.randString(25),
                    rating = randGen.randNumber(0,10),
                    date_submitted = randGen.randDate()
                };

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
