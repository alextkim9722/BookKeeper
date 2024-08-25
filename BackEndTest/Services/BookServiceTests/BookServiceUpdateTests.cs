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

namespace BackEndTest.Services.BookServiceTests
{
	[Collection("Service Tests")]
	public class BookServiceUpdateTests : IClassFixture<BookDatabaseGenerator>
	{
		private readonly BookService _bookService;
		private readonly BookShelfContext _bookShelfContext;

		public BookServiceUpdateTests(BookDatabaseGenerator generator)
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
		public void UpdateBook_Is1WithNoName_ReturnsFailureResult()
		{
			var id = 1;
			var errorMsg = "[System.String[]] Title is required!";
			var updatedBook = new Book()
			{
				pKey = id,
				pages = 120,
				isbn = "1ajfespdntle3",
				cover_picture = "red path",
			};

			var result = _bookService.UpdateBook(id, updatedBook);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMsg, result.msg);
		}
		[Fact]
		public void UpdateBook_Is1WithNoPageCount_ReturnsFailureResult()
		{
			var id = 1;
			var errorMsg = "[System.String[]] Page length exceeds maximum value or is less than or equal to 0!";
			var updatedBook = new Book()
			{
				pKey = id,
				title = "Red Book",
				isbn = "1ajfespdntle3",
				cover_picture = "red path",
			};

			var result = _bookService.UpdateBook(id, updatedBook);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMsg, result.msg);
		}
		[Fact]
		public void UpdateBook_Is1WithNoISBN_ReturnsFailureResult()
		{
			var id = 1;
			var errorMsg = "[System.String[]] ISBN is required!";
			var updatedBook = new Book()
			{
				pKey = id,
				title = "Red Book",
				pages = 120,
				cover_picture = "red path",
			};

			var result = _bookService.UpdateBook(id, updatedBook);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMsg, result.msg);
		}
		[Fact]
		public void UpdateBook_Is1WithNoCover_ReturnsFailureResult()
		{
			var id = 1;
			var errorMsg = "[System.String[]] Cover Picture is required!";
			var updatedBook = new Book()
			{
				pKey = id,
				title = "Red Book",
				pages = 120,
				isbn = "1ajfespdntle3"
			};

			var result = _bookService.UpdateBook(id, updatedBook);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMsg, result.msg);
		}
	}
}
