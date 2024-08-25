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
using Moq;
using Microsoft.Data.SqlClient;

namespace BackEndTest.Services.AuthorServiceTests
{
	[Collection("Service Tests")]
	public class AuthorServiceReadTests : IClassFixture<AuthorDatabaseGenerator>
	{
		private readonly AuthorService _authorService;
		private readonly BookShelfContext _bookShelfContext;

		public AuthorServiceReadTests(AuthorDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_authorService = new AuthorService(
				new GenericService<Author>(_bookShelfContext),
				new JunctionService<Book_Author>(_bookShelfContext));
		}

		[Fact]
		public void GetAuthorById_Is1_ReturnsSuccessfulResult()
		{
			var id = 1;
			var booksWritten = 2;
			var expectedAuthor = new Author()
			{
				pKey = id,
				first_name = "Deborah",
				middle_name = "Nemona",
				last_name = "White"
			};
			var expectedFullName = expectedAuthor.first_name + " " + expectedAuthor.middle_name + " " + expectedAuthor.last_name;

			var result = _authorService.GetAuthorById(id);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(expectedAuthor, result.payload);
			Assert.Equal(expectedFullName, result.payload.full_name);
			Assert.Equal(booksWritten, result.payload.books.Count());
		}
		[Fact]
		public void GetAuthorById_Is2_ReturnsSuccessfulResultWithNoMiddleName()
		{
			var id = 2;
			var booksWritten = 1;
			var expectedAuthor = new Author()
			{
				pKey = id,
				first_name = "James",
				last_name = "Remmington"
			};
			var expectedFullName = expectedAuthor.first_name + " " + expectedAuthor.last_name;

			var result = _authorService.GetAuthorById(id);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(expectedAuthor, result.payload);
			Assert.Equal(expectedFullName, result.payload.full_name);
			Assert.Equal(booksWritten, result.payload.books.Count());
		}
		[Fact]
		public void GetAuthorByFirstName_IsDeborah_ReturnsSuccessfulResult()
		{
			var first = "Deborah";
			var booksWritten = 2;
			var expectedAuthor = new Author()
			{
				pKey = 1,
				first_name = "Deborah",
				middle_name = "Nemona",
				last_name = "White"
			};
			var expectedFullName = expectedAuthor.first_name + " " + expectedAuthor.middle_name + " " + expectedAuthor.last_name;

			var result = _authorService.GetAuthorByFirstName(first);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(expectedAuthor, result.payload.FirstOrDefault());
			Assert.Equal(expectedFullName, result.payload.FirstOrDefault().full_name);
			Assert.Equal(booksWritten, result.payload.FirstOrDefault().books.Count());
		}
		[Fact]
		public void GetAuthorByMiddleName_IsNemona_ReturnsSuccessfulResult()
		{
			var middle = "Nemona";
			var booksWritten = 2;
			var expectedAuthor = new Author()
			{
				pKey = 1,
				first_name = "Deborah",
				middle_name = "Nemona",
				last_name = "White"
			};
			var expectedFullName = expectedAuthor.first_name + " " + expectedAuthor.middle_name + " " + expectedAuthor.last_name;

			var result = _authorService.GetAuthorByMiddleName(middle);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(expectedAuthor, result.payload.FirstOrDefault());
			Assert.Equal(expectedFullName, result.payload.FirstOrDefault().full_name);
			Assert.Equal(booksWritten, result.payload.FirstOrDefault().books.Count());
		}
		[Fact]
		public void GetAuthorByLastName_IsWhite_ReturnsSuccessfulResult()
		{
			var last = "White";
			var booksWritten = 2;
			var expectedAuthor = new Author()
			{
				pKey = 1,
				first_name = "Deborah",
				middle_name = "Nemona",
				last_name = "White"
			};
			var expectedFullName = expectedAuthor.first_name + " " + expectedAuthor.middle_name + " " + expectedAuthor.last_name;

			var result = _authorService.GetAuthorByLastName(last);

			Assert.True(result.success);
			MappedComparator.CompareAuthor(expectedAuthor, result.payload.FirstOrDefault());
			Assert.Equal(expectedFullName, result.payload.FirstOrDefault().full_name);
			Assert.Equal(booksWritten, result.payload.FirstOrDefault().books.Count());
		}
		[Fact]
		public void GetAuthorById_Is5_ReturnsFailedResult()
		{
			var id = 5;
			var errorMsg = "[ERROR]: Models were not found!" + Environment.NewLine;

			var result = _authorService.GetAuthorById(id);

			Assert.False(result.success);
			Assert.Equal(errorMsg, result.msg);
		}
		[Fact]
		public void GetAuthorByFirstName_IsBob_ReturnsFailedResult()
		{
			var first = "Bob";
			var errorMsg = "[ERROR]: Models were not found!" + Environment.NewLine;

			var result = _authorService.GetAuthorByFirstName(first);

			Assert.False(result.success);
			Assert.Equal(errorMsg, result.msg);
		}
		[Fact]
		public void GetAuthorByMiddleName_IsJenny_ReturnsFailedResult()
		{
			var middle = "Jenny";
			var errorMsg = "[ERROR]: Models were not found!" + Environment.NewLine;

			var result = _authorService.GetAuthorByMiddleName(middle);

			Assert.False(result.success);
			Assert.Equal(errorMsg, result.msg);
		}
		[Fact]
		public void GetAuthorByLastName_IsSheen_ReturnsFailedResult()
		{
			var last = "Sheen";
			var errorMsg = "[ERROR]: Models were not found!" + Environment.NewLine;

			var result = _authorService.GetAuthorByLastName(last);

			Assert.False(result.success);
			Assert.Equal(errorMsg, result.msg);
		}
	}
}
