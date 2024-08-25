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
using BackEnd.Services.Interfaces;

namespace BackEndTest.Services.GenreServiceTests
{
	[Collection("Service Tests")]
	public class GenreServiceDeleteTests : IClassFixture<GenreDatabaseGenerator>
	{
		private readonly GenreService _genreService;
		private readonly BookShelfContext _bookShelfContext;

		public GenreServiceDeleteTests(GenreDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_genreService = new GenreService(
				new GenericService<Genre>(_bookShelfContext),
				new JunctionService<Book_Genre>(_bookShelfContext));
		}

		[Fact]
		public void RemoveGenre_Is1_ResultsSuccessfulFindResultsFailure()
		{
			var id = 1;

			var result = _genreService.RemoveGenre([id]);

			Assert.True(result.success);
			Assert.Null(_bookShelfContext.Author.Find(id));
		}
	}
}
