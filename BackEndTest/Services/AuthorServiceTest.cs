using BackEnd.Model;
using BackEnd.Services;
using BackEnd.Services.ErrorHandling;
using BackEnd.Services.Interfaces;
using BackEndTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
	[Collection("Test Integration With DB")]
	public class AuthorServiceTest : IClassFixture<TestDatabaseGenerator>
	{
		private readonly AuthorService _authorService;

		// Test database services by comparing values between list data
		// and database/context data.
		public AuthorServiceTest(TestDatabaseGenerator generator)
		{
			var context = generator.createContext();
			_authorService = new AuthorService(context);
		}

		private void authorEquals(Author expected, Author actual)
		{
			MappedComparator.compareAuthor(expected, actual);

			Assert.Equal(expected.full_name, actual.full_name);

			for (int i = 0; i < expected.books.Count(); i++)
			{
				MappedComparator.compareBook(
					expected.books.ElementAt(i), actual.books.ElementAt(i));
			}
		}

		[Fact]
		public void GetAllAuthors_InvokedWithValidName_ReturnsAllUsersWithNoNulls()
		{
			AuthorTheoryDataGenerator data = new AuthorTheoryDataGenerator();
			Results<IEnumerable<Author>> actual = _authorService.getAllAuthors();

			Assert.True(actual.success);

			for (int i = 0; i < data.Count(); i++)
			{
				authorEquals((Author)data.ElementAt(i).FirstOrDefault(), actual.payload.ElementAt(i));
			}
		}

		[Theory]
		[ClassData(typeof(AuthorTheoryDataGenerator))]
		public void GetAuthorById_InvokedWithValidId_ReturnsUserWithNoNulls(Author expected)
		{
			Results<Author> actual = _authorService.getAuthorById(expected.author_id);

			Assert.True(actual.success);
			authorEquals(expected, actual.payload);
		}

		[Theory]
		[ClassData(typeof(AuthorTheoryDataGenerator))]
		public void GetAuthorById_InvokedWithValidFirstName_ReturnsUserWithNoNulls(Author expected)
		{
			Results<Author> actual = _authorService.getAuthorByFirstName(expected.first_name);

			Assert.True(actual.success);
			authorEquals(expected, actual.payload);
		}

		[Theory]
		[ClassData(typeof(AuthorTheoryDataGenerator))]
		public void GetAuthorById_InvokedWithValidMiddleName_ReturnsUserWithNoNulls(Author expected)
		{
			Results<Author> actual = _authorService.getAuthorByMiddleName(expected.middle_name);

			Assert.True(actual.success);
			authorEquals(expected, actual.payload);
		}

		[Theory]
		[ClassData(typeof(AuthorTheoryDataGenerator))]
		public void GetAuthorById_InvokedWithValidLastName_ReturnsUserWithNoNulls(Author expected)
		{
			Results<Author> actual = _authorService.getAuthorByLastName(expected.last_name);

			Assert.True(actual.success);
			authorEquals(expected, actual.payload);
		}

		[Fact]
		public void AddAuthor_InvokedWithValidAuthorWithNoId_ReturnsSuccessResultAndExistsInDatabase()
		{
			var expected = new Author()
			{
				first_name = "Nicole",
				middle_name = "Valentine",
				last_name = "Blake"
			};

			Results<Author> add = _authorService.addAuthor(expected);
			Results<Author> actual = _authorService.getAuthorByFirstName("Nicole");

			Assert.True(add.success);
			MappedComparator.compareAuthor(expected, actual.payload);

			_authorService.removeAuthor(actual.payload.author_id);
		}

		[Fact]
		public void UpdateAuthor_InvokedWithProperIdAndUpdatedAuthorWithSameId_ReturnsSuccessResult()
		{
			AuthorTheoryDataGenerator data = new AuthorTheoryDataGenerator();

			var expected = new Author()
			{
				author_id = 2,
				first_name = "Nicole",
				middle_name = "Valentine",
				last_name = "Blake"
			};

			var original = new Author()
			{
				author_id = 2,
				first_name = TestDatabaseGenerator.authorTable[1].first_name,
				middle_name = TestDatabaseGenerator.authorTable[1].middle_name,
				last_name = TestDatabaseGenerator.authorTable[1].last_name,
			};

			Results<Author> update = _authorService.updateAuthor(2, expected);
			Results<Author> actual = _authorService.getAuthorById(2);

			Assert.True(update.success);
			MappedComparator.compareAuthor(expected, actual.payload);

			_authorService.updateAuthor(2, original);
		}

		[Fact]
		public void GetAuthorById_InvokedWithNonExistantId_ReturnsFailedResult()
		{
			Results<Author> actual = _authorService.getAuthorById(50);

			Assert.NotNull(actual);
			Assert.False(actual.success);
		}
	}
}
