using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System;

using MainProject.Datastore;
using MainProject.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			_bookShelfContext.Database.ExecuteSql($"TRUNCATE TABLE [dbo].[user_book]");

            _bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[user]");
			_bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[book]");
			_bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[genre]");
			_bookShelfContext.Database.ExecuteSql($"DELETE [dbo].[author]");
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
                    [RATING],
                    [COVER_PICTURE])
                VALUES
                    (0, 'Blue Book', 200, 'blueisbn', 10, '/Images-Covers/bluebook.png'),
                    (1, 'Red Book', 479, 'redisbn', 7, '/Images-Covers/redbook.png'),
                    (2, 'Green Book', 392, 'greenisbn', 2, '/Images-Covers/greenbook.png'),
                    (3, 'Yellow Book', 890, 'yellowisbn', 6, '/Images-Covers/yellowbook.png'),
                    (4, 'Purple Book', 628, 'purpleisbn', 8, '/Images-Covers/purplebook.png');
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
		}
    }
}
