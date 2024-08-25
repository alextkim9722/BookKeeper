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
	public class AuthorServiceUpdateTests : IClassFixture<AuthorDatabaseGenerator>
	{
		private readonly AuthorService _authorService;
		private readonly BookShelfContext _bookShelfContext;

		public AuthorServiceUpdateTests(AuthorDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_authorService = new AuthorService(
				new GenericService<Author>(_bookShelfContext),
				new JunctionService<Book_Author>(_bookShelfContext));
		}

		[Fact]
		public void UpdateAuthor_Is2WithFirstNameGeorge_ReturnsSuccessulResult()
		{
			var id = 2;
			var updatedModel = new Author()
			{
				pKey = id,
				first_name = "George",
				last_name = "Remmington"
			};

			var result = _authorService.UpdateAuthor(id, updatedModel);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(updatedModel, result.payload);
			MappedComparator.CompareAuthor(updatedModel, _bookShelfContext.Author.Find(id));
		}
		[Fact]
		public void UpdateAuthor_Is3WithMiddleNameRachel_ReturnsSuccessulResult()
		{
			var id = 3;
			var updatedModel = new Author()
			{
				pKey = id,
				first_name = "Blair",
				middle_name = "Rachel",
				last_name = "Project"
			};

			var result = _authorService.UpdateAuthor(id, updatedModel);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(updatedModel, result.payload);
			MappedComparator.CompareAuthor(updatedModel, _bookShelfContext.Author.Find(id));
		}
		[Fact]
		public void UpdateAuthor_Is3WithLastNameRed_ReturnsSuccessulResult()
		{
			var id = 3;
			var updatedModel = new Author()
			{
				pKey = id,
				first_name = "Blair",
				middle_name = "Rachel",
				last_name = "Red"
			};

			var result = _authorService.UpdateAuthor(id, updatedModel);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(updatedModel, result.payload);
			MappedComparator.CompareAuthor(updatedModel, _bookShelfContext.Author.Find(id));
		}
		[Fact]
		public void UpdateAuthor_Is3WithNoMiddle_ReturnsSuccessulResult()
		{
			var id = 3;
			var updatedModel = new Author()
			{
				pKey = id,
				first_name = "Blair",
				last_name = "Red"
			};

			var result = _authorService.UpdateAuthor(id, updatedModel);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(updatedModel, result.payload);
			MappedComparator.CompareAuthor(updatedModel, _bookShelfContext.Author.Find(id));
		}
		[Fact]
		public void UpdateAuthor_Is1WithNoFirst_ReturnsFailedResult()
		{
			var errorMessage = "[ERROR]: [System.String[]] First Name is required!" + Environment.NewLine;
			var id = 1;
			var authorModel = new Author()
			{
				pKey = id,
				middle_name = "Nemona",
				last_name = "White"
			};

			var result = _authorService.UpdateAuthor(id, authorModel);

			Assert.False(result.success);
			Assert.Equal(errorMessage, result.msg);
		}
		[Fact]
		public void UpdateAuthor_Is1WithNoLast_ReturnsFailedResult()
		{
			var errorMessage = "[ERROR]: [System.String[]] Last Name is required!" + Environment.NewLine;
			var id = 1;
			var authorModel = new Author()
			{
				pKey = id,
				first_name = "Deborah",
				middle_name = "Nemona"
			};

			var result = _authorService.UpdateAuthor(id, authorModel);

			Assert.False(result.success);
			Assert.Equal(errorMessage, result.msg);
		}
		[Fact]
		public void UpdateAuthor_Is200WithLastNameRed_ReturnsFailedResult()
		{
			var errorMessage = "[ERROR]: Could not find model!" + Environment.NewLine;
			var id = 200;
			var authorModel = new Author()
			{
				pKey = id,
				first_name = "Blair",
				middle_name = "Witch",
				last_name = "Red"
			};

			var result = _authorService.UpdateAuthor(id, authorModel);

			Assert.False(result.success);
			Assert.Equal(errorMessage, result.msg);
		}
	}
}
