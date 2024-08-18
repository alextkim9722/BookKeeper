using BackEnd.Model;
using BackEnd.Services;
using BackEndTest.Services;
using BackEnd.Services.ErrorHandling;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace MainProjectTest.Services
{
	[Collection("Test Integration With DB")]
    public class BookServiceTest : IClassFixture<TestDatabaseGenerator>
	{
		private readonly TestDatabaseGenerator _generator;
		private readonly BookService _bookService;
		private bool testDataGenerated = false;

		// Test database services by comparing values between list data
		// and database/context data.
		public BookServiceTest(TestDatabaseGenerator generator)
		{
			_generator = generator;
			var context = generator.createContext();
			_bookService = new BookService(context);
			BookTheoryDataGenerator.generator = generator;
		}

		private void bookEquals(Book expectedBook, Book actualBook)
		{
			Assert.Equal(expectedBook.book_id, actualBook.book_id);
			Assert.Equal(expectedBook.title, actualBook.title);
			Assert.Equal(expectedBook.cover_picture, actualBook.cover_picture);
			Assert.Equal(expectedBook.pages, actualBook.pages);
			Assert.Equal(expectedBook.isbn, actualBook.isbn);
			Assert.Equal(expectedBook.rating, actualBook.rating);
			Assert.Equal(expectedBook.authors.Count(), actualBook.authors.Count());
			Assert.Equal(expectedBook.genres.Count(), actualBook.genres.Count());
			Assert.Equal(expectedBook.reviews.Count(), actualBook.reviews.Count());

			for(int i = 0;i < expectedBook.authors.Count();i++)
			{
				Assert.Equal(expectedBook.authors.ElementAt(i).author_id, expectedBook.authors.ElementAt(i).author_id);
				Assert.Equal(expectedBook.authors.ElementAt(i).first_name, expectedBook.authors.ElementAt(i).first_name);
				Assert.Equal(expectedBook.authors.ElementAt(i).middle_name, expectedBook.authors.ElementAt(i).middle_name);
				Assert.Equal(expectedBook.authors.ElementAt(i).last_name, expectedBook.authors.ElementAt(i).last_name);
			}

			for (int i = 0; i < expectedBook.genres.Count(); i++)
			{
				Assert.Equal(expectedBook.genres.ElementAt(i).genre_id, expectedBook.genres.ElementAt(i).genre_id);
				Assert.Equal(expectedBook.genres.ElementAt(i).genre_name, expectedBook.genres.ElementAt(i).genre_name);
			}

			for (int i = 0; i < expectedBook.reviews.Count(); i++)
			{
				Assert.Equal(expectedBook.reviews.ElementAt(i).book_id, expectedBook.reviews.ElementAt(i).book_id);
				Assert.Equal(expectedBook.reviews.ElementAt(i).user_id, expectedBook.reviews.ElementAt(i).user_id);
				Assert.Equal(expectedBook.reviews.ElementAt(i).description, expectedBook.reviews.ElementAt(i).description);
				Assert.Equal(expectedBook.reviews.ElementAt(i).date_submitted, expectedBook.reviews.ElementAt(i).date_submitted);
				Assert.Equal(expectedBook.reviews.ElementAt(i).rating, expectedBook.reviews.ElementAt(i).rating);
			}
		}

		[Theory]
		[ClassData(typeof(BookTheoryDataGenerator))]
		public void GET_A_SINGLE_BOOK_FROM_THE_DATABASE(Book expectedBook)
		{
			Results<Book> actualBook = _bookService.getBookById(expectedBook.book_id);

			Assert.True(actualBook.success);
			bookEquals(expectedBook, actualBook.payload);
		}

		[Theory]
		[ClassData(typeof(BookTheoryDataGenerator))]
		public void GET_A_SINGLE_BOOK_FROM_THE_DATABASE_ISBN(Book expectedBook)
		{
			Book? actualBook = _bookService.getBookByIsbn(expectedBook.isbn).payload;

			Assert.Equal(expectedBook, actualBook);
		}

		[Theory]
		[ClassData(typeof(BookTheoryDataGenerator))]
		public void GET_A_SINGLE_BOOK_FROM_THE_DATABASE_TITLE(Book expectedBook)
		{
			Book? actualBook = _bookService.getBookByTitle(expectedBook.title).payload;

			Assert.Equal(expectedBook, actualBook);
		}

		[Fact]
		public void ADD_A_SINGLE_BOOK_INTO_THE_DATABASE()
		{
			Book expectedBook = new Book()
			{
				title = "NewBook",
				pages = 143,
				isbn = "isbnnumber",
				cover_picture = "/image/path.jpg"
			};

			Book? addedBook = _bookService.addBook(expectedBook)!.payload;

			Book? actualBook = _bookService.getBookByTitle("NewBook").payload;

			Assert.Equal(expectedBook, actualBook);
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

			Book? updatedBook = _bookService.updateBook(2, expectedBook).payload;

			Book? actualBook = _bookService.getBookById(2).payload;

			Assert.NotNull(updatedBook);
			Assert.Equal(expectedBook, actualBook);
		}
	}
}
