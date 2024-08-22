using BackEnd.Model;
using BackEnd.Services;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using BackEndTest.Services.TheoryDataGenerators;
using BackEndTest.Services.RandomGenerators;
using BackEnd.ErrorHandling;

namespace BackEndTest.Services
{
    [Collection("Test Integration With DB")]
	public class BookServiceTest : IClassFixture<TestDatabaseGenerator>
	{
		private readonly BookService _bookService;
		private readonly TestDatabaseGenerator _testDatabaseGenerator;

		// Test database services by comparing values between list data
		// and database/context data.
		public BookServiceTest(TestDatabaseGenerator generator)
		{
			var context = generator.createContext();
			_bookService = new BookService(context);
			_testDatabaseGenerator = generator;
		}

		private void bookEquals(Book expectedBook, Book actualBook)
		{
			MappedComparator.compareBook(expectedBook, actualBook);

			Assert.Equal(expectedBook.readers, actualBook.readers);

			for(int i = 0;i < expectedBook.authors.Count();i++)
			{
				MappedComparator.compareAuthor(
					expectedBook.authors.ElementAt(i), actualBook.authors.ElementAt(i));
			}

			for (int i = 0; i < expectedBook.genres.Count(); i++)
			{
				MappedComparator.compareGenre(
					expectedBook.genres.ElementAt(i), actualBook.genres.ElementAt(i));
			}

			for (int i = 0; i < expectedBook.reviews.Count(); i++)
			{
				MappedComparator.compareReview(
					expectedBook.reviews.ElementAt(i), actualBook.reviews.ElementAt(i));
			}
		}

		[Fact]
		public void GetAllBooks_InvokedWithValidName_ReturnsAllBooksWithNoNulls()
		{
			BookTheoryDataGenerator data = new BookTheoryDataGenerator();
			Results<IEnumerable<Book>> actualBook = _bookService.getAllBooks();

			Assert.True(actualBook.success);

			for (int i = 0;i < data.Count();i++)
			{
				bookEquals((Book)data.ElementAt(i).FirstOrDefault(), actualBook.payload.ElementAt(i));
			}
		}

		[Theory]
		[ClassData(typeof(BookTheoryDataGenerator))]
		public void GetBookById_InvokedWithValidId_ReturnsBookWithNoNulls(Book expectedBook)
		{
			Results<Book> actualBook = _bookService.getBookById(expectedBook.book_id);

			Assert.True(actualBook.success);
			bookEquals(expectedBook, actualBook.payload);
		}

		[Theory]
		[ClassData(typeof(BookTheoryDataGenerator))]
		public void GetBookById_InvokedWithValidIsbn_ReturnsBookWithNoNulls(Book expectedBook)
		{
			Results<Book> actualBook = _bookService.getBookByIsbn(expectedBook.isbn);

			Assert.True(actualBook.success);
			bookEquals(expectedBook, actualBook.payload);
		}

		[Theory]
		[ClassData(typeof(BookTheoryDataGenerator))]
		public void GetBookById_InvokedWithValidTitle_ReturnsBookWithNoNulls(Book expectedBook)
		{
			Results<Book> actualBook = _bookService.getBookByTitle(expectedBook.title);

			Assert.True(actualBook.success);
			bookEquals(expectedBook, actualBook.payload);
		}

		[Fact]
		public void AddBook_InvokedWithValidBookWithNoId_ReturnsSuccessResultAndExistsInDatabase()
		{
			Book expectedBook = new Book()
			{
				title = "NewBook",
				pages = 143,
				isbn = "isbnnumber",
				cover_picture = "/image/path.jpg"
			};

			Results<Book> addedBook = _bookService.addBook(expectedBook);
			Results<Book> actualBook = _bookService.getBookByTitle("NewBook");

			Assert.True(addedBook.success);
			MappedComparator.compareBook(expectedBook, actualBook.payload);

			_bookService.removeBook(actualBook.payload.book_id);
		}

		[Fact]
		public void UpdateBook_InvokedWithProperIdAndUpdatedBookWithSameId_ReturnsSuccessResult()
		{
			Book expectedBook = new Book()
			{
				book_id = 2,
				title = "Green Yellow Book",
				pages = 392,
				isbn = "greenisbn",
				cover_picture = "/Images-Covers/greenbook.png"
			};

			var original = new Book()
			{
				book_id = 2,
				title = TestDatabaseGenerator.bookTable[1].title,
				pages = TestDatabaseGenerator.bookTable[1].pages,
				isbn = TestDatabaseGenerator.bookTable[1].isbn,
				cover_picture = TestDatabaseGenerator.bookTable[1].cover_picture,
			};

			Results<Book> updatedBook = _bookService.updateBook(2, expectedBook);
			Results<Book> actualBook = _bookService.getBookById(2);

			Assert.True(updatedBook.success);
			MappedComparator.compareBook(expectedBook, actualBook.payload);

			_bookService.updateBook(2, original);
		}

		[Fact]
		public void GetBookById_InvokedWithNonExistantId_ReturnsFailedResult()
		{
			Results<Book> actualBook = _bookService.getBookById(50);

			Assert.NotNull(actualBook);
			Assert.False(actualBook.success);
		}
	}
}
