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
using BackEndTest.Services.Comparator;

namespace BackEndTest.Services.BookServiceTests
{
	[Collection("Service Tests")]
	public class BookServiceCreateTests : IClassFixture<BookDatabaseGenerator>
	{
		private readonly BookService _bookService;
		private readonly BookShelfContext _bookShelfContext;

		public BookServiceCreateTests(BookDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_bookService = new BookService(
				new GenericService<Book>(_bookShelfContext),
				new JunctionService<Book_Author>(_bookShelfContext),
				new JunctionService<Book_Genre>(_bookShelfContext),
				new JunctionService<User_Book>(_bookShelfContext),
				new JunctionService<Review>(_bookShelfContext));
		}

		[Fact]
		public void AddBook_InBookModel_ResultsSuccessfulFindSuccessful()
		{
			var bookModel = new Book
			{
				title = "Yellow Book",
				pages = 120,
				isbn = "aaaaaaaaaaaaa",
				cover_picture = "yellow path"
			};

			var result = _bookService.AddBook(bookModel);

			Assert.True(result.success);
			MappedComparator.CompareBook(bookModel, result.payload);
			MappedComparator.CompareBook(_bookShelfContext.Book.OrderBy(x => x.pKey).LastOrDefault(), result.payload);
		}
		[Fact]
		public void AddBook_InBookModelWithISBNWith10Digits_ResultsFailureWithErrorMsg()
		{
			var errorMessage = "[System.String[]] ISBN length not within 13 digit standard!";
			var bookModel = new Book
			{
				title = "Yellow Book",
				pages = 120,
				isbn = "aaaaaaaa",
				cover_picture = "yellow path"
			};

			var result = _bookService.AddBook(bookModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddBook_InBookModelWithISBNWith15Digits_ResultsFailureWithErrorMsg()
		{
			var errorMessage = "[System.String[]] ISBN length not within 13 digit standard!";
			var bookModel = new Book
			{
				title = "Yellow Book",
				pages = 120,
				isbn = "aaaaaaaaaaaaaaa",
				cover_picture = "yellow path"
			};

			var result = _bookService.AddBook(bookModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddBook_InBookModelWithoutTitle_ResultsFailureWithErrorMsg()
		{
			var errorMessage = "[System.String[]] Title is required!";
			var bookModel = new Book
			{
				pages = 120,
				isbn = "aaaaaaaaaaaaa",
				cover_picture = "yellow path"
			};

			var result = _bookService.AddBook(bookModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddBook_InBookModelWithoutPages_ResultsFailureWithErrorMsg()
		{
			var errorMessage = "[System.String[]] Page length exceeds maximum value or is less than or equal to 0!";
			var bookModel = new Book
			{
				title = "Yellow Book",
				isbn = "aaaaaaaaaaaaa",
				cover_picture = "yellow path"
			};

			var result = _bookService.AddBook(bookModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddBook_InBookModelWithoutISBN_ResultsFailureWithErrorMsg()
		{
			var errorMessage = "[System.String[]] ISBN is required!";
			var bookModel = new Book
			{
				title = "Yellow Book",
				pages = 120,
				cover_picture = "yellow path"
			};

			var result = _bookService.AddBook(bookModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddBook_InBookModelWithoutCover_ResultsFailureWithErrorMsg()
		{
			var errorMessage = "[System.String[]] Cover Picture is required!";
			var bookModel = new Book
			{
				title = "Yellow Book",
				pages = 120,
				isbn = "aaaaaaaaaaaaa"
			};

			var result = _bookService.AddBook(bookModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
	}
}
