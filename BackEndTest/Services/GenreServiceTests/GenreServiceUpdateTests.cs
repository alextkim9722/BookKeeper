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
	public class GenreServiceUpdateTests : IClassFixture<GenreDatabaseGenerator>
	{
		private readonly GenreService _genreService;
		private readonly BookShelfContext _bookShelfContext;

		public GenreServiceUpdateTests(GenreDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_genreService = new GenreService(
				new GenericService<Genre>(_bookShelfContext),
				new JunctionService<Book_Genre>(_bookShelfContext));
		}

		[Fact]
		public void UpdateGenre_IsId1_ResultsSuccessful()
		{
			var id = 1;
			var updatedModel = new Genre()
			{
				pKey = id,
				genre_name = "horror"
			};

			var result = _genreService.UpdateGenre(id, updatedModel);

			Assert.True(result.success);
			MappedComparator.CompareGenre(_bookShelfContext.Genre.Find(id), result.payload);
		}
		[Fact]
		public void UpdateGenre_IsId1NoName_ResultsSuccessful()
		{
			var id = 1;
			var errorMessage = "[System.String[]] The genre name is required!";
			var updatedModel = new Genre()
			{
				pKey = 1
			};

			var result = _genreService.UpdateGenre(id, updatedModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
	}
}
