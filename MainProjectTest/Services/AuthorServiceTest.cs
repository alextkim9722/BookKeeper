using MainProject.Model;
using MainProject.Services;
using MainProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProjectTest.Services
{
	// There are a lot of similarities between the Service classes.
	// Im only going to test one of the get functions as I want to
	// see if the callback is working properly with multiple delegates.
	public class AuthorServiceTest : IClassFixture<TestDatabaseFixture>
	{
		private readonly BookShelfContext _bookShelfContext;
		private readonly AuthorService _authorService;

		private readonly TestDatabaseFixture _Fixture;

		public AuthorServiceTest(TestDatabaseFixture fixture)
		{
			_Fixture = fixture;
			_bookShelfContext = _Fixture.createContext();
			_Fixture.clearTables(_bookShelfContext);
			_Fixture.populateTables(_bookShelfContext);
			_Fixture.populateBridgeTables(_bookShelfContext);

			_authorService = new AuthorService(_bookShelfContext);
		}

		#region SETUP
		private void READ_SETUP(
			ref Author expectedAuthor,
			ref IEnumerable<Book>? expectedBooks
			)
		{
			expectedAuthor = new Author()
			{
				author_id = 1,
				first_name = "Nicole",
				middle_name = "Valentine",
				last_name = "Terantino"
			};

			expectedBooks = _bookShelfContext.Book
				.Where(x => _bookShelfContext.Book_Author
				.Where(y => y.author_id == 1)
				.Select(y => y.book_id)
				.ToList()
				.Contains(x.book_id))
				.ToList();
		}
		#endregion

		#region CHECKS
		private void READ_AUTHOR(
			Author expectedAuthor,
			Author? actualAuthor
			)
		{
			string expectedFullName = expectedAuthor.first_name
				+ " " + expectedAuthor.middle_name
				+ " " + expectedAuthor.last_name;

			Assert.NotNull(actualAuthor);
			Assert.Equal(expectedAuthor.author_id, actualAuthor.author_id);
			Assert.Equal(expectedAuthor.first_name, actualAuthor.first_name);
			Assert.Equal(expectedAuthor.middle_name, actualAuthor.middle_name);
			Assert.Equal(expectedAuthor.last_name, actualAuthor.last_name);
			Assert.Equal(expectedFullName, actualAuthor.full_name);
		}

		private void READ_BRIDGES(
			Author? actualAuthor,
			IEnumerable<Book>? expectedBooks
			)
		{
			Assert.NotNull(actualAuthor.books);
			Assert.Equal(expectedBooks, actualAuthor.books);
		}
		#endregion

		[Fact]
		public void GET_A_SINGLE_AUTHOR_FROM_THE_DATABASE()
		{
			Author expectedAuthor = new Author();
			IEnumerable<Book>? expectedBooks = null;

			READ_SETUP(ref expectedAuthor, ref expectedBooks);

			Author? actualAuthor = _authorService.getAuthorById(1);

			READ_AUTHOR(expectedAuthor, actualAuthor);
			READ_BRIDGES(actualAuthor, expectedBooks);
		}
	}
}
