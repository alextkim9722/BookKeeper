using BackEnd.Model;
using BackEnd.Services.Context;
using BackEnd.Services.Generics;
using BackEnd.Services;
using BackEndTest.Services.DatabaseGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEndTest.Services.Comparator;

namespace BackEndTest.Services.GenreServiceTests
{
	[Collection("Service Tests")]
	public class GenreServiceReadTests : IClassFixture<GenreDatabaseGenerator>
	{
		private readonly GenreService _genreService;
		private readonly BookShelfContext _bookShelfContext;

		public GenreServiceReadTests(GenreDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_genreService = new GenreService(
				new GenericService<Genre>(_bookShelfContext),
				new JunctionService<Book_Genre>(_bookShelfContext));
		}

		[Fact]
		public void GetGenreById_Is1_ResultsSuccessful()
		{
			var id = 1;
			var books = 3;
			var genreModel = new Genre()
			{
				pKey = id,
				genre_name = "scifi"
			};

			var result = _genreService.GetGenreById(id);

			Assert.True(result.success);
			MappedComparator.CompareGenre(genreModel, result.payload);
			Assert.Equal(books, result.payload.books.Count());
		}
		[Fact]
		public void GetGenreByName_IsScifi_ResultsSuccessful()
		{
			var id = 1;
			var books = 3;
			var genreModel = new Genre()
			{
				pKey = id,
				genre_name = "scifi"
			};

			var result = _genreService.GetGenreByName("scifi");

			Assert.True(result.success);
			MappedComparator.CompareGenre(genreModel, result.payload.FirstOrDefault());
			Assert.Equal(books, result.payload.FirstOrDefault().books.Count());
		}
	}
}
