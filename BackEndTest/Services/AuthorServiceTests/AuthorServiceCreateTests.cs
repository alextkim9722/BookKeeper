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

namespace BackEndTest.Services.AuthorServiceTests
{
	[Collection("Service Tests")]
	public class AuthorServiceCreateTests : IClassFixture<AuthorDatabaseGenerator>
	{
		private readonly AuthorService _authorService;
		private readonly BookShelfContext _bookShelfContext;

		public AuthorServiceCreateTests(AuthorDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_authorService = new AuthorService(
				new GenericService<Author>(_bookShelfContext),
				new JunctionService<Book_Author>(_bookShelfContext));
		}

		[Fact]
		public void AddAuthor_IsAuthorModel_ReturnsSuccessfulResult()
		{
			var authorModel = new Author()
			{
				first_name = "William",
				middle_name = "Charlie",
				last_name = "Grace"
			};

			var result = _authorService.AddAuthor(authorModel);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(authorModel, result.payload);
			MappedComparator.CompareAuthor(_bookShelfContext.Author.OrderBy(x => x.pKey).LastOrDefault(), result.payload);
		}
		[Fact]
		public void AddAuthor_IsAuthorModelWithNoMiddle_ReturnsSuccessfulResult()
		{
			var authorModel = new Author()
			{
				first_name = "William",
				last_name = "Grace"
			};

			var result = _authorService.AddAuthor(authorModel);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(authorModel, result.payload);
			MappedComparator.CompareAuthor(_bookShelfContext.Author.OrderBy(x => x.pKey).LastOrDefault(), result.payload);
		}
		[Fact]
		public void AddAuthor_IsAuthorModelWithNoFirstName_ReturnsFailedResult()
		{
			var errorMessage = "[ERROR]: [System.String[]] First Name is required!" + Environment.NewLine;
			var authorModel = new Author()
			{
				middle_name = "Charlie",
				last_name = "Grace"
			};

			var result = _authorService.AddAuthor(authorModel);

			Assert.False(result.success);
			Assert.Equal(errorMessage, result.msg);
		}
		[Fact]
		public void AddAuthor_IsAuthorModelWithNoLastName_ReturnsFailedResult()
		{
			var errorMessage = "[ERROR]: [System.String[]] Last Name is required!" + Environment.NewLine;
			var authorModel = new Author()
			{
				first_name = "William",
				middle_name = "Charlie"
			};

			var result = _authorService.AddAuthor(authorModel);

			Assert.False(result.success);
			Assert.Equal(errorMessage, result.msg);
		}
	}
}
