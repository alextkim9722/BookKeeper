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
using BackEnd.Services.Generics.Interfaces;
using Xunit.Sdk;

namespace BackEndTest.Services.BookServiceTests
{
	[Collection("Service Tests")]
	public class BookServiceReadTests : IClassFixture<BookDatabaseGenerator>
	{
		private readonly BookService _bookService;
		private readonly BookShelfContext _bookShelfContext;

		public BookServiceReadTests(BookDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_bookService = new BookService(
				new GenericService<Book>(_bookShelfContext),
				new JunctionService<Book_Author>(_bookShelfContext),
				new JunctionService<Book_Genre>(_bookShelfContext),
				new JunctionService<User_Book>(_bookShelfContext),
				new JunctionService<Review>(_bookShelfContext));
		}

		public void CompareAuthorsGenresUsersReviews(int authors, int genres, int users, int reviews, Book right)
		{
			Assert.Equal(authors, right.authors.Count());
			Assert.Equal(genres, right.genres.Count());
			Assert.Equal(users, right.users.Count());
			Assert.Equal(reviews, right.reviews.Count());
		}

		[Fact]
		public void GetBookById_Is1_ResultsSuccessfulAndMatchWithExpected()
		{
			var id = 1;
			var authors = 1;
			var genres = 1;
			var users = 3;
			var reviews = 3;
			var expectedBook = new Book()
			{
				pKey = id,
				title = "Red Book",
				pages = 120,
				isbn = "1ajfespdntle3",
				cover_picture = "red path",
			};

			var result = _bookService.GetBookById(id);

			Assert.True(result.success);
			MappedComparator.CompareBook(expectedBook, result.payload);
			CompareAuthorsGenresUsersReviews(authors, genres, users, reviews, result.payload);
		}
		[Fact]
		public void GetBookByIsbn_Islajfespdntle3_ResultsSuccessfulAndMatchWithExpected()
		{
			var isbn = "1ajfespdntle3";
			var authors = 1;
			var genres = 1;
			var users = 3;
			var reviews = 3;
			var expectedBook = new Book()
			{
				pKey = 1,
				title = "Red Book",
				pages = 120,
				isbn = isbn,
				cover_picture = "red path",
			};

			var result = _bookService.GetBookByIsbn(isbn);

			Assert.True(result.success);
			MappedComparator.CompareBook(expectedBook, result.payload);
			CompareAuthorsGenresUsersReviews(authors, genres, users, reviews, result.payload);
		}
		[Fact]
		public void GetBookByTitle_IsRedBook_ResultsSuccessfulAndMatchWithExpected()
		{
			var title = "Red Book";
			var authors = 1;
			var genres = 1;
			var users = 3;
			var reviews = 3;
			var expectedBook = new Book()
			{
				pKey = 1,
				title = "Red Book",
				pages = 120,
				isbn = "1ajfespdntle3",
				cover_picture = "red path",
			};

			var result = _bookService.GetBookByTitle(title);

			Assert.True(result.success);
			MappedComparator.CompareBook(expectedBook, result.payload.FirstOrDefault());
			CompareAuthorsGenresUsersReviews(authors, genres, users, reviews, result.payload.FirstOrDefault());
		}
		[Fact]
		public void SetRating_IsBook1AndReviewsFind1_ResultsSuccessfulAndMatchRatingWithExpected()
		{
			var id = 1;
			var book = _bookService.GetBookById(id);
			var reviewList = _bookShelfContext.Review.Where(x => x.secondKey == id).ToList();
			var expectedRating = 3;

			var rating = _bookService.SetRating(book.payload, reviewList);

			Assert.True(rating.success);
			Assert.Equal(expectedRating, book.payload.rating);
		}
		[Fact]
		public void SetRating_IsBook1AndReviewsFull_ResultsFailure()
		{
			var id = 1;
			var book = _bookService.GetBookById(id);
			var errorMessage = "Too many reviews!";
			var reviewList = _bookShelfContext.Review.ToList();

			var rating = _bookService.SetRating(book.payload, reviewList);

			Assert.False(rating.success);
			ErrorComparator.CompareErrors(errorMessage, rating.msg);
		}
		[Fact]
		public void SetRating_IsBook1AndMismatchedKey_ResultsSuccessfulAndMatchRatingWithExpected()
		{
			var id = 1;
			var book = _bookService.GetBookById(id);
			var errorMessage = "Not all reviews are from readers who read the book!";
			var reviewList = _bookShelfContext.Review.Where(x => x.secondKey == id).ToList();
			reviewList.ToArray()[0].firstKey = 1000;

			var rating = _bookService.SetRating(book.payload, reviewList);

			Assert.False(rating.success);
			ErrorComparator.CompareErrors(errorMessage, rating.msg);
		}
	}
}
