using BackEnd.Model;
using BackEnd.Services;
using BackEnd.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProjectTest.Services
{
	[Collection("Test Integration With DB")]
	public class UserServiceTest : IClassFixture<TestDatabaseFixture>
	{
		//private readonly BookShelfContext _bookShelfContext;
		//private readonly UserService _userService;

		//private readonly TestDatabaseFixture _Fixture;

		//public UserServiceTest(TestDatabaseFixture fixture)
		//{
		//	_Fixture = fixture;
		//	_bookShelfContext = _Fixture.createContext();
		//	_Fixture.clearTables(_bookShelfContext);
		//	_Fixture.populateTables(_bookShelfContext);
		//	_Fixture.populateBridgeTables(_bookShelfContext);
		//	TestDatabaseFixture.cleared = false;

		//	_userService = new UserService(_bookShelfContext);
		//}

		//#region SETUP
		//private void READ_SETUP(
		//	ref User expectedUser,
		//	ref IEnumerable<Book>? expectedBooks
		//	)
		//{
		//	expectedUser = new User()
		//	{
		//		user_id = 0,
		//		username = "alberto153",
		//		description = "My name is alberto and I like to read.",
		//		date_joined = new DateOnly(2014, 2, 11),
		//		profile_picture = "images/TempProfilePic.jpg"
		//	};

		//	expectedBooks = _bookShelfContext.Book
		//		.Where(x => _bookShelfContext.User_Book
		//		.Where(y => y.user_id == 0)
		//		.Select(y => y.book_id)
		//		.ToList()
		//		.Contains(x.book_id))
		//		.ToList();
		//}
		//#endregion

		//#region CHECKS
		//private void READ_AUTHOR(
		//	User expectedUser,
		//	User? actualUser
		//	)
		//{
		//	Assert.NotNull(actualUser);
		//	Assert.Equal(expectedUser.user_id, actualUser.user_id);
		//	Assert.Equal(expectedUser.username, actualUser.username);
		//	Assert.Equal(expectedUser.date_joined, actualUser.date_joined);
		//	Assert.Equal(expectedUser.description, actualUser.description);
		//	Assert.Equal(expectedUser.profile_picture, actualUser.profile_picture);
		//}

		//private void READ_BRIDGES(
		//	User? actualUser,
		//	IEnumerable<Book>? expectedBooks
		//	)
		//{
		//	Assert.NotNull(actualUser.books);
		//	Assert.Equal(expectedBooks, actualUser.books);
		//}
		//#endregion

		//[Fact]
		//public void GET_A_SINGLE_USER_FROM_THE_DATABASE()
		//{
		//	User expectedUser = new User();
		//	IEnumerable<Book>? expectedBooks = null;

		//	READ_SETUP(ref expectedUser, ref expectedBooks);

		//	User? actualUser = _userService.getUserById(0);

		//	READ_AUTHOR(expectedUser, actualUser);
		//	READ_BRIDGES(actualUser, expectedBooks);
		//}
	}
}
