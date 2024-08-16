using BackEnd.Model;
using BackEnd.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProjectTest.Services
{
	[Collection("Test Integration With DB")]
    public class BookServiceTest : IClassFixture<TestDatabaseFixture>
	{
		private readonly BookShelfContext _bookShelfContext;
		private readonly BookService _bookService;

		private readonly TestDatabaseFixture _Fixture;

		public BookServiceTest(TestDatabaseFixture fixture)
		{
			_Fixture = fixture;
			_bookShelfContext = _Fixture.createContext();
			_Fixture.clearTables(_bookShelfContext);
			_Fixture.populateTables(_bookShelfContext);
			_Fixture.populateBridgeTables(_bookShelfContext);
			TestDatabaseFixture.cleared = false;

			_bookService = new BookService(_bookShelfContext);
		}

		#region SETUP
		private void READ_SETUP(
			ref Book expectedBook,
			ref IEnumerable<Author>? expectedAuthors,
			ref IEnumerable<Genre>? expectedGenres,
			ref int? expectedReaders,
			ref IEnumerable<Review>? expectedReviews
			)
		{
			expectedBook = new Book()
			{
				book_id = 2,
				title = "Green Book",
				pages = 392,
				isbn = "greenisbn",
				rating = 2,
				cover_picture = "/Images-Covers/greenbook.png"
			};

			expectedAuthors = _bookShelfContext.Author.Where(
				x => _bookShelfContext.Book_Author
				.Where(y => y.book_id == 2)
				.Select(y => y.author_id)
				.ToList()
				.Contains(x.author_id))
				.ToList();

			expectedGenres = _bookShelfContext.Genre.Where(
				x => _bookShelfContext.Book_Genre
				.Where(y => y.book_id == 2)
				.Select(y => y.genre_id)
				.ToList()
				.Contains(x.genre_id))
				.ToList();

			expectedReaders = 3;

			expectedReviews = _bookShelfContext.Review
				.Where(y => y.book_id == 2)
				.ToList();
		}
		#endregion

		#region CHECKS
		private void READ_BOOK(
			Book expectedBook,
			Book? actualBook
			)
		{
			Assert.NotNull(actualBook);
			Assert.Equal(expectedBook.book_id, actualBook.book_id);
			Assert.Equal(expectedBook.title, actualBook.title);
			Assert.Equal(expectedBook.pages, actualBook.pages);
			Assert.Equal(expectedBook.isbn, actualBook.isbn);
			Assert.Equal(expectedBook.cover_picture, actualBook.cover_picture);
		}

		private void READ_BRIDGES(
			Book expectedBook,
			Book? actualBook,
			IEnumerable<Author>? expectedAuthors, 
			IEnumerable<Genre>? expectedGenres, 
			int? expectedReaders,
			IEnumerable<Review>? expectedReviews
			)
		{
			Assert.NotNull(actualBook.authors);
			Assert.NotNull(actualBook.genres);
			Assert.NotNull(actualBook.readers);
			Assert.NotNull(actualBook.reviews);
			Assert.Equal(expectedAuthors, actualBook.authors);
			Assert.Equal(expectedGenres, actualBook.genres);
			Assert.Equal(expectedReaders, actualBook.readers);
			Assert.Equal(expectedReviews, actualBook.reviews);
		}
		#endregion

		[Fact]
		public void GET_ALL_BOOKS()
		{
			Book expectedBook = new Book();
			IEnumerable<Author>? expectedAuthors = null;
			IEnumerable<Genre>? expectedGenres = null;
			IEnumerable<Review>? expectedReviews = null;
			int? expectedReaders = 0;

			READ_SETUP(ref expectedBook, ref expectedAuthors, ref expectedGenres, ref expectedReaders, ref expectedReviews);

			IEnumerable<Book>? actualBook = _bookService.getAllBooks();

			READ_BOOK(expectedBook, actualBook.ElementAt(2));
			READ_BRIDGES(expectedBook, actualBook.ElementAt(2), expectedAuthors, expectedGenres, expectedReaders, expectedReviews);
		}

		[Fact]
		public void GET_A_SINGLE_BOOK_FROM_THE_DATABASE()
		{
			Book expectedBook = new Book();
			IEnumerable<Author>? expectedAuthors = null;
			IEnumerable<Genre>? expectedGenres = null;
			IEnumerable<Review>? expectedReviews = null;
			int? expectedReaders = 0;

			READ_SETUP(ref expectedBook, ref expectedAuthors, ref expectedGenres, ref expectedReaders, ref expectedReviews);

			Book? actualBook = _bookService.getBookById(2);

			READ_BOOK(expectedBook, actualBook);
			READ_BRIDGES(expectedBook, actualBook, expectedAuthors, expectedGenres, expectedReaders, expectedReviews);
		}

		[Fact]
		public void GET_A_SINGLE_BOOK_FROM_THE_DATABASE_ISBN()
		{
			Book expectedBook = new Book();
			IEnumerable<Author>? expectedAuthors = null;
			IEnumerable<Genre>? expectedGenres = null;
			IEnumerable<Review>? expectedReviews = null;
			int? expectedReaders = 0;

			READ_SETUP(ref expectedBook, ref expectedAuthors, ref expectedGenres, ref expectedReaders, ref expectedReviews);

			Book? actualBook = _bookService.getBookByIsbn("greenisbn");

			READ_BOOK(expectedBook, actualBook);
			READ_BRIDGES(expectedBook, actualBook, expectedAuthors, expectedGenres, expectedReaders, expectedReviews);
		}

		[Fact]
		public void GET_A_SINGLE_BOOK_FROM_THE_DATABASE_TITLE()
		{
			Book expectedBook = new Book();
			IEnumerable<Author>? expectedAuthors = null;
			IEnumerable<Genre>? expectedGenres = null;
			IEnumerable<Review>? expectedReviews = null;
			int? expectedReaders = 0;

			READ_SETUP(ref expectedBook, ref expectedAuthors, ref expectedGenres, ref expectedReaders, ref expectedReviews);

			Book? actualBook = _bookService.getBookByTitle("Green Book");

			READ_BOOK(expectedBook, actualBook);
			READ_BRIDGES(expectedBook, actualBook, expectedAuthors, expectedGenres, expectedReaders, expectedReviews);
		}

		[Fact]
		public void ADD_A_SINGLE_BOOK_INTO_THE_DATABASE()
		{
			Book expectedBook = new Book()
			{
				title = "TanBook",
				pages = 143,
				isbn = "isbnnumber",
				cover_picture = "/image/path.jpg"
			};

			Book addedBook = _bookService.addBook(expectedBook)!;

			Book? actualBook = _bookService.getBookById(
				_bookShelfContext.Book.OrderBy(x => x.book_id).LastOrDefault().book_id
			);

			Assert.NotNull(addedBook);
			READ_BOOK(expectedBook, actualBook);
		}

		[Fact]
		public void REMOVE_A_SINGLE_BOOK_IN_THE_DATABASE()
		{
			Book? expectedBook = _bookService.removeBook(2);

			Book? actualBook = _bookService.getBookById(2);

			Assert.NotNull(expectedBook);
			Assert.Null(actualBook);
		}

		[Fact]
		public void EDIT_A_MAPPED_PROPERTY_IN_A_BOOK()
		{
			Book expectedBook = new Book()
			{
				book_id = 2,
				title = "Green Yellow Book",
				pages = 392,
				isbn = "greenisbn",
				rating = 2,
				cover_picture = "/Images-Covers/greenbook.png"
			};

			Book? updatedBook = _bookService.updateBook(2, expectedBook);

			Book? actualBook = _bookService.getBookById(2);

			Assert.NotNull(updatedBook);
			READ_BOOK(expectedBook, actualBook);
		}
	}
}
