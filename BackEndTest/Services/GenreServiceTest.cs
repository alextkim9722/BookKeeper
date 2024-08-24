using BackEnd.Model;
using BackEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEndTest.Services.TheoryDataGenerators;
using BackEndTest.Services.RandomGenerators;
using BackEnd.Services.ErrorHandling;

namespace BackEndTest.Services
{
    [Collection("Test Integration With DB")]
	public class GenreServiceTest : IClassFixture<TestDatabaseGenerator>
	{
		private readonly GenreService _genreService;

		// Test database services by comparing values between list data
		// and database/context data.
		public GenreServiceTest(TestDatabaseGenerator generator)
		{
			var context = generator.createContext();
			_genreService = new GenreService(context);
		}

		private void genreEquals(Genre expected, Genre actual)
		{
			MappedComparator.compareGenre(expected, actual);

			for (int i = 0; i < expected.books.Count(); i++)
			{
				MappedComparator.compareBook(
					expected.books.ElementAt(i), actual.books.ElementAt(i));
			}
		}

		[Fact]
		public void GetAllGenres_InvokedWithValidName_ReturnsAllUsersWithNoNulls()
		{
			Results<IEnumerable<Genre>> actual = _genreService.getAllGenres();
			List<Genre> expected = TestDatabaseGenerator.genreTable
				.OrderBy(x => x.genre_id).ToList();

			Assert.True(actual.success);

			for (int i = 0; i < expected.Count(); i++)
			{
				genreEquals(expected[i], actual.payload.ElementAt(i));
			}
		}

		[Theory]
		[ClassData(typeof(GenreTheoryDataGenerator))]
		public void GetGenreById_InvokedWithValidId_ReturnsGenreWithNoNulls(Genre expected)
		{
			Results<Genre> actual = _genreService.getGenreById(expected.genre_id);

			Assert.True(actual.success);
			genreEquals(expected, actual.payload);
		}

		[Theory]
		[ClassData(typeof(GenreTheoryDataGenerator))]
		public void GetGenreByName_InvokedWithValidName_ReturnsUserWithNoNulls(Genre expected)
		{
			Results<Genre> actual = _genreService.getGenreByName(expected.genre_name);

			Assert.True(actual.success);
			genreEquals(expected, actual.payload);
		}

		[Fact]
		public void AddGenre_InvokedWithValidGenreWithNoId_ReturnsSuccessResultAndExistsInDatabase()
		{
			var expected = new Genre()
			{
				genre_name = "borgar"
			};

			Results<Genre> add = _genreService.addGenre(expected);
			Results<Genre> actual = _genreService.getGenreByName("borgar");

			Assert.True(add.success);
			MappedComparator.compareGenre(expected, actual.payload);

			_genreService.removeGenre(actual.payload.genre_id);
		}

		[Fact]
		public void UpdateGenre_InvokedWithProperIdAndUpdatedGenreWithSameId_ReturnsSuccessResult()
		{
			GenreTheoryDataGenerator data = new GenreTheoryDataGenerator();

			var expected = new Genre()
			{
				genre_id = 2,
				genre_name = "borgar"
			};

			var original = new Genre()
			{
				genre_id = 2,
				genre_name = TestDatabaseGenerator.genreTable[2].genre_name
			};

			Results<Genre> update = _genreService.updateGenre(2, expected);
			Results<Genre> actual = _genreService.getGenreById(2);

			Assert.True(update.success);
			MappedComparator.compareGenre(expected, actual.payload);

			_genreService.updateGenre(2, original);
		}

		[Fact]
		public void GetGenreById_InvokedWithNonExistantId_ReturnsFailedResult()
		{
			Results<Genre> actual = _genreService.getGenreById(50);

			Assert.NotNull(actual);
			Assert.False(actual.success);
		}
	}
}
