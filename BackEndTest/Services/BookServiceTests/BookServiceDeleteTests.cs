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

namespace BackEndTest.Services.BookServiceTests
{
	[Collection("Service Tests")]
	public class BookServiceDeleteTests : IClassFixture<BookDatabaseGenerator>
	{
		private readonly BookService _bookService;
		private readonly BookShelfContext _bookShelfContext;

		public BookServiceDeleteTests(BookDatabaseGenerator generator)
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
		public void RemoveBooks_IsId1_ResultsSuccessfulFailureToFind()
		{
			var id = 1;

			var result = _bookService.RemoveBooks([id]);

			Assert.True(result.success);
			Assert.Null(_bookShelfContext.Book.Find(id));
		}
	}
}
