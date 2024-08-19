using BackEnd.Model;
using BackEnd.Services;
using BackEnd.Services.ErrorHandling;
using BackEnd.Services.Interfaces;
using BackEndTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
	[Collection("Test Integration With DB")]
	public class UserServiceTest : IClassFixture<TestDatabaseGenerator>
	{
		private readonly UserService _userService;

		// Test database services by comparing values between list data
		// and database/context data.
		public UserServiceTest(TestDatabaseGenerator generator)
		{
			var context = generator.createContext();
			_userService = new UserService(context);
		}

		private void userEquals(User expected, User actual)
		{
			MappedComparator.compareUser(expected, actual);

			Assert.Equal(expected.pagesRead, actual.pagesRead);

			for (int i = 0; i < expected.books.Count(); i++)
			{
				MappedComparator.compareBook(
					expected.books.ElementAt(i), actual.books.ElementAt(i));
			}

			for (int i = 0; i < expected.reviews.Count(); i++)
			{
				MappedComparator.compareReview(
					expected.reviews.ElementAt(i), actual.reviews.ElementAt(i));
			}
		}

		[Fact]
		public void GetAllUsers_InvokedWithValidName_ReturnsAllUsersWithNoNulls()
		{
			UserTheoryDataGenerator data = new UserTheoryDataGenerator();
			Results<IEnumerable<User>> actual = _userService.getAllUser();

			Assert.True(actual.success);

			for (int i = 0; i < data.Count(); i++)
			{
				userEquals((User)data.ElementAt(i).FirstOrDefault(), actual.payload.ElementAt(i));
			}
		}

		[Theory]
		[ClassData(typeof(UserTheoryDataGenerator))]
		public void GetUserById_InvokedWithValidId_ReturnsUserWithNoNulls(User expected)
		{
			Results<User> actual = _userService.getUserById(expected.user_id);

			Assert.True(actual.success);
			userEquals(expected, actual.payload);
		}

		[Theory]
		[ClassData(typeof(UserTheoryDataGenerator))]
		public void GetUserByUsername_InvokedWithValidUsername_ReturnsUserWithNoNulls(User expected)
		{
			Results<User> actual = _userService.getUserByUserName(expected.username);

			Assert.True(actual.success);
			userEquals(expected, actual.payload);
		}

		[Fact]
		public void AddUser_InvokedWithValidUserWithNoId_ReturnsSuccessResultAndExistsInDatabase()
		{
			var expected = new User()
			{
				username = "bob",
				identification_id = string.Empty,
				date_joined = new DateOnly(2022,02,02),
				description = "asdf",
				profile_picture = "fsda"
			};

			Results<User> add = _userService.addUser(expected);
			Results<User> actual = _userService.getUserByUserName("bob");

			Assert.True(add.success);
			MappedComparator.compareUser(expected, actual.payload);

			_userService.removeUser(actual.payload.user_id);
		}

		[Fact]
		public void UpdateUser_InvokedWithProperIdAndUpdatedUserWithSameId_ReturnsSuccessResult()
		{
			UserTheoryDataGenerator data = new UserTheoryDataGenerator();

			var expected = new User()
			{
				user_id = 2,
				username = "bob",
				identification_id = "",
				date_joined = new DateOnly(2022, 02, 02),
				description = "asdf",
				profile_picture = "fsda"
			};

			var original = new User()
			{
				user_id = 2,
				username = TestDatabaseGenerator.userTable[1].username,
				identification_id = TestDatabaseGenerator.userTable[1].identification_id,
				date_joined = TestDatabaseGenerator.userTable[1].date_joined,
				description = TestDatabaseGenerator.userTable[1].description,
				profile_picture = TestDatabaseGenerator.userTable[1].profile_picture
			};

			Results<User> update = _userService.updateUser(2, expected);
			Results<User> actual = _userService.getUserById(2);

			Assert.True(update.success);
			MappedComparator.compareUser(expected, actual.payload);

			_userService.updateUser(2, original);
		}

		[Fact]
		public void GetUserById_InvokedWithNonExistantId_ReturnsFailedResult()
		{
			Results<User> actual = _userService.getUserById(50);

			Assert.NotNull(actual);
			Assert.False(actual.success);
		}
	}
}
