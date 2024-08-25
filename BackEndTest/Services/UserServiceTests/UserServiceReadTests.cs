using BackEnd.Model;
using BackEnd.Services.Context;
using BackEnd.Services.Generics;
using BackEnd.Services;
using BackEndTest.Services.DatabaseGenerators;
using BackEndTest.Services.RandomGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEndTest.Services.Comparator;
using BackEnd.Services.Interfaces;

namespace BackEndTest.Services.UserServiceTests
{
	[Collection("Service Tests")]
	public class UserServiceReadTests : IClassFixture<UserDatabaseGenerator>
	{
		private readonly UserService _userService;
		private readonly BookShelfContext _bookShelfContext;
		private ValueGenerators randGen = new ValueGenerators();

		public UserServiceReadTests(UserDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_userService = new UserService(
				new GenericService<User>(_bookShelfContext),
				new JunctionService<User_Book>(_bookShelfContext),
				new JunctionService<Review>(_bookShelfContext));
		}

		private void CompareBooksReviews(int books, int reviews, User right)
		{
			Assert.Equal(books, right.books.Count());
			Assert.Equal(reviews, right.reviews.Count());
		}

		[Fact]
		public void GetUserById_IsId1_ResultSuccessful()
		{
			var id = 1;
			var books = 2;
			var reviews = 2;
			var expectedUser = new User()
			{
				pKey = id,
				username = "redBro",
				identification_id = _bookShelfContext.Users.OrderBy(x => x.Id).FirstOrDefault().Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			var result = _userService.GetUserById(id);

			Assert.True(result.success);
			MappedComparator.CompareUser(expectedUser, result.payload);
			CompareBooksReviews(books, reviews, result.payload);
		}
		[Fact]
		public void GetUserByIdentificationId_IsIdentificationId1_ResultSuccessful()
		{
			var id = 1;
			var books = 2;
			var reviews = 2;
			var expectedUser = new User()
			{
				pKey = id,
				username = "redBro",
				identification_id = _bookShelfContext.Users.OrderBy(x => x.Id).FirstOrDefault().Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			var result = _userService.GetUserByIdentificationId(expectedUser.identification_id);

			Assert.True(result.success);
			MappedComparator.CompareUser(expectedUser, result.payload);
			CompareBooksReviews(books, reviews, result.payload);
		}
		[Fact]
		public void GetUserByUserName_IsRedBro_ResultSuccessful()
		{
			var username = "redBro";
			var books = 2;
			var reviews = 2;
			var expectedUser = new User()
			{
				pKey = 1,
				username = "redBro",
				identification_id = _bookShelfContext.Users.OrderBy(x => x.Id).FirstOrDefault().Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			var result = _userService.GetUserByUserName(username);

			Assert.True(result.success);
			MappedComparator.CompareUser(expectedUser, result.payload.FirstOrDefault());
			CompareBooksReviews(books, reviews, result.payload.FirstOrDefault());
		}
		[Fact]
		public void SetBooksAndPagesRead_IsUser1AndBooks_ResultsSuccessful()
		{
			var id = 1;
			var book = _userService.GetUserById(id);
			var bookIdList = _bookShelfContext.User_Book.Where(x => x.firstKey == id).Select(x => x.secondKey).ToList();
			var bookList = _bookShelfContext.Book.Where(x => bookIdList.Contains(x.pKey)).ToList();
			var expectedPages = 15;
			var expectedBooks = 2;

			var result = _userService.SetBooksAndPagesRead(book.payload, bookList);

			Assert.True(result.success);
			Assert.Equal(expectedBooks, book.payload.booksRead);
			Assert.Equal(expectedPages, book.payload.pagesRead);
		}
		[Fact]
		public void SetBooksAndPagesRead_IsUser1AndBooksFull_ResultsFailure()
		{
			var errorMessage = "Too many books!";
			var id = 1;
			var book = _userService.GetUserById(id);
			var bookList = _bookShelfContext.Book.ToList();

			var result = _userService.SetBooksAndPagesRead(book.payload, bookList);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void SetBooksAndPagesRead_IsUser1AndMismatchedKey_ResultsSuccessfulAndMatchRatingWithExpected()
		{
			var errorMessage = "Not all books are read by the user!";
			var id = 1;
			var book = _userService.GetUserById(id);
			var bookIdList = _bookShelfContext.User_Book.Where(x => x.firstKey == id).Select(x => x.secondKey).ToList();
			var bookList = _bookShelfContext.Book.Where(x => bookIdList.Contains(x.pKey)).ToList();
			bookList[0].pKey = 1000;

			var result = _userService.SetBooksAndPagesRead(book.payload, bookList);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
	}
}
