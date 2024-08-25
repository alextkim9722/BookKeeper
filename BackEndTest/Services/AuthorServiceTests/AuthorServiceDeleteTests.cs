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

namespace BackEndTest.Services.AuthorServiceTests
{
	[Collection("Service Tests")]
	public class AuthorServiceDeleteTests : IClassFixture<AuthorDatabaseGenerator>
	{
		private readonly AuthorService _authorService;
		private readonly BookShelfContext _bookShelfContext;

		public AuthorServiceDeleteTests(AuthorDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_authorService = new AuthorService(
				new GenericService<Author>(_bookShelfContext),
				new JunctionService<Book_Author>(_bookShelfContext));
		}

		[Fact]
		public void RemoveAuthor_Is1_ResultsSuccessfulFindResultsFailure()
		{
			var id = 1;

			var result = _authorService.RemoveAuthor([id]);

			Assert.True(result.success);
			Assert.Null(_bookShelfContext.Author.Find(id));
		}
		[Fact]
		public void RemoveAuthor_Is300_ResultsFailure()
		{
			var errorMsg = "[ERROR]: No models to delete were found!" + Environment.NewLine;
			var id = 300;

			var result = _authorService.RemoveAuthor([id]);

			Assert.False(result.success);
			Assert.Equal(errorMsg, result.msg);
		}
	}
}
