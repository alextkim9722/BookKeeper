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
	public class GenreServiceCreateTests : IClassFixture<GenreDatabaseGenerator>
	{
		private readonly GenreService _genreService;
		private readonly BookShelfContext _bookShelfContext;

		public GenreServiceCreateTests(GenreDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_genreService = new GenreService(
				new GenericService<Genre>(_bookShelfContext),
				new JunctionService<Book_Genre>(_bookShelfContext));
		}

		[Fact]
		public void AddGenre_IsGenreModel_ResultsSuccessful()
		{
			var genreModel = new Genre()
			{
				genre_name = "horror"
			};

			var result = _genreService.AddGenre(genreModel);

			Assert.True(result.success);
			MappedComparator.CompareGenre(_bookShelfContext.Genre.OrderBy(x => x.pKey).LastOrDefault(), result.payload);
		}
		[Fact]
		public void AddGenre_IsGenreNoName_ResultsSuccessful()
		{
			var errorMessage = "[System.String[]] The genre name is required!";
			var genreModel = new Genre();

			var result = _genreService.AddGenre(genreModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
	}
}
