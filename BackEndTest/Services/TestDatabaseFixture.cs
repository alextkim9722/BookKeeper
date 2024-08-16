using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System;
using BackEnd.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BackEnd.Services;

namespace MainProjectTest.Services
{
    public class TestDatabaseFixture
    {
        private const string connectionString = "Server=DESKTOP-550OG8P\\MSSQLSERVER2022;Database=BookKeeperDB_Test;Trusted_Connection=True;TrustServerCertificate=True";

        public TestDatabaseFixture()
        {
			using (var _bookShelfContext = createContext())
			{
				clearTables(_bookShelfContext);
				populateTables(_bookShelfContext);
				populateBridgeTables(_bookShelfContext);
			}
		}
        public BookShelfContext createContext()
        => new BookShelfContext(
            new DbContextOptionsBuilder<BookShelfContext>()
            .UseSqlServer(connectionString)
            .Options);

        public void clearTables(BookShelfContext _bookShelfContext)
        {
			_bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[book_author]");
			_bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[book_genre]");
			_bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[book_review]");
			_bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[user_book]");
			_bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[user_review]");

			_bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[user]");
			_bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[book]");
			_bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[genre]");
			_bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[author]");
			_bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[review]");
		}

        public void populateTables(BookShelfContext _bookShelfContext)
		{
			_bookShelfContext.Database.ExecuteSql(
                $@"
                SET IDENTITY_INSERT [dbo].[genre] ON
                INSERT INTO [dbo].[genre]
                    ([GENRE_ID],
                    [GENRE_NAME])
                VALUES
                    (0,'sci-fi'),
                    (1,'fantasy'),
                    (2,'horror'),
                    (3,'action'),
                    (4,'adventure'),
                    (5,'encyclopedia')
                SET IDENTITY_INSERT [dbo].[genre] OFF"
                );
            _bookShelfContext.Database.ExecuteSql(
                $@"
                SET IDENTITY_INSERT [dbo].[author] ON
                INSERT INTO [dbo].[author]
                    ([AUTHOR_ID],
                    [FIRST_NAME],
                    [MIDDLE_NAME],
                    [LAST_NAME])
                VALUES
                    (0, 'Martha', 'Nora', 'Smith'),
                    (1, 'Nicole', 'Valentine', 'Terantino'),
                    (2, 'Derik', 'Tanner', 'Washington'),
                    (3, 'William', 'Richard', 'Newton');
                SET IDENTITY_INSERT [dbo].[author] OFF"
                );
            _bookShelfContext.Database.ExecuteSql(
                $@"
                SET IDENTITY_INSERT [dbo].[book] ON
                INSERT INTO [dbo].[book]
                    ([BOOK_ID],
                    [TITLE],
                    [PAGES],
                    [ISBN],
                    [COVER_PICTURE])
                VALUES
                    (0, 'Blue Book', 200, 'blueisbn', '/Images-Covers/bluebook.png'),
                    (1, 'Red Book', 479, 'redisbn', '/Images-Covers/redbook.png'),
                    (2, 'Green Book', 392, 'greenisbn', '/Images-Covers/greenbook.png'),
                    (3, 'Yellow Book', 890, 'yellowisbn', '/Images-Covers/yellowbook.png'),
                    (4, 'Purple Book', 628, 'purpleisbn', '/Images-Covers/purplebook.png');
                SET IDENTITY_INSERT [dbo].[book] OFF"
                );
            _bookShelfContext.Database.ExecuteSql(
                $@"
                SET IDENTITY_INSERT [dbo].[user] ON
                INSERT INTO [dbo].[user]
                    ([USER_ID],
                    [USERNAME],
                    [DESCRIPTION],
                    [DATE_JOINED],
                    [PROFILE_PICTURE])
                VALUES
                    (0,'alberto153','My name is alberto and I like to read.','2014-02-11','images/TempProfilePic.jpg'),
                    (1,'bertthebart751','Reading books are my life.','2008-05-23','images/profilepic1.jpg'),
                    (2,'palpal8457','Let us be friends.','2022-10-01','images/profilepic2.jpg'),
                    (3,'MariaCanza123','Wassup guys! How are you all doing','2012-12-12','images/profilepic3.jpg')
                SET IDENTITY_INSERT [dbo].[user] OFF"
                );
			_bookShelfContext.Database.ExecuteSql(
				$@"
                SET IDENTITY_INSERT [dbo].[review] ON
                INSERT INTO [dbo].[review]
                    ([REVIEW_ID],
                    [DESCRIPTION],
                    [RATING],
                    [DATE_SUBMITTED])
                VALUES
                    (0, 'review 0', 2, '2014-01-02'),
                    (1, 'review 1', 4, '2014-01-03'),
                    (2, 'review 2', 1, '2014-01-04'),
                    (3, 'review 3', 7, '2014-01-05'),
                    (4, 'review 4', 4, '2014-01-06'),
                    (5, 'review 5', 9, '2014-01-07'),
                    (6, 'review 6', 3, '2014-01-08'),
                    (7, 'review 7', 10, '2014-01-09'),
                    (8, 'review 8', 2, '2014-01-12'),
                    (9, 'review 9', 6, '2014-01-18'),
                    (10, 'review 10', 10, '2014-01-13'),
                    (11, 'review 11', 5, '2014-01-14'),
                    (12, 'review 12', 9, '2014-01-15'),
                    (13, 'review 13', 5, '2014-01-16'),
                    (14, 'review 14', 3, '2014-01-17'),
                SET IDENTITY_INSERT [dbo].[review] OFF"
				);
		}

        public void populateBridgeTables(BookShelfContext _bookShelfContext)
        {
			_bookShelfContext.Database.ExecuteSql(
				$@"INSERT INTO [dbo].[book_author]
                    ([BOOK_ID],
                    [AUTHOR_ID])
                VALUES
                    (0,0),
                    (1,1),
                    (2,3),
                    (3,2),
                    (0,3),
                    (4,2),
                    (3,3),
                    (4,3)"
				);
			_bookShelfContext.Database.ExecuteSql(
				$@"INSERT INTO [dbo].[book_genre]
                    ([BOOK_ID],
                    [GENRE_ID])
                VALUES
                    (0,1),
                    (1,2),
                    (2,3),
                    (3,4),
                    (0,4),
                    (4,4),
                    (3,5),
                    (4,5),
                    (0,0),
                    (4,2),
                    (3,3)"
				);
			_bookShelfContext.Database.ExecuteSql(
				$@"INSERT INTO [dbo].[user_book]
                    ([USER_ID],
                    [BOOK_ID])
                VALUES
                    (0,1),
                    (1,2),
                    (2,3),
                    (3,4),
                    (0,4),
                    (3,2),
                    (3,3),
                    (2,2),
                    (1,1)"
				);
			_bookShelfContext.Database.ExecuteSql(
				$@"INSERT INTO [dbo].[user_review]
                    ([USER_ID],
                    [REVIEW_ID])
                VALUES
                    (0,1),
                    (2,2),
                    (1,3),
                    (3,4),
                    (0,5),
                    (1,6),
                    (0,7),
                    (2,8),
                    (1,9),
                    (2,10),
                    (1,11),
                    (3,12),
                    (0,13),
                    (3,14),
                    (3,15),"
				);
			_bookShelfContext.Database.ExecuteSql(
				$@"INSERT INTO [dbo].[book_review]
                    ([BOOK_ID],
                    [REVIEW_ID])
                VALUES
                    (0,1),
                    (2,2),
                    (1,3),
                    (3,4),
                    (4,5),
                    (1,6),
                    (0,7),
                    (2,8),
                    (1,9),
                    (2,10),
                    (1,11),
                    (4,12),
                    (0,13),
                    (4,14),
                    (3,15),"
				);
		}
    }
}
