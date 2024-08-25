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
using Moq;
using BackEndTest.Services.RandomGenerators;

namespace BackEndTest.Services.UserServiceTests
{
	[Collection("Service Tests")]
	public class UserServiceCreateTests : IClassFixture<UserDatabaseGenerator>
	{
		private readonly UserService _userService;
		private readonly BookShelfContext _bookShelfContext;
		private ValueGenerators randGen = new ValueGenerators();

		public UserServiceCreateTests(UserDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_userService = new UserService(
				new GenericService<User>(_bookShelfContext),
				new JunctionService<User_Book>(_bookShelfContext),
				new JunctionService<Review>(_bookShelfContext));
		}

		[Fact]
		public void AddUser_IsUserModel_ResultsSuccessful()
		{
			var identification = new Identification()
			{
				Id = randGen.randString(250),
				EmailConfirmed = true,
				LockoutEnabled = false,
				TwoFactorEnabled = false,
				Email = randGen.randString(100) + "@" + "asdfmail.com",
				UserName = randGen.randString(250)
			};
			var userModel = new User
			{
				username = "magentaBro",
				identification_id = identification.Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			_bookShelfContext.Users.Add(identification);
			var result = _userService.AddUser(userModel);

			Assert.True(result.success);
			MappedComparator.CompareUser(userModel, result.payload);
			MappedComparator.CompareUser(_bookShelfContext.User.OrderBy(x => x.pKey).LastOrDefault(), result.payload);
		}
		[Fact]
		public void AddUser_IsUserModelNoDescription_ResultsSuccessful()
		{
			var identification = new Identification()
			{
				Id = randGen.randString(250),
				EmailConfirmed = true,
				LockoutEnabled = false,
				TwoFactorEnabled = false,
				Email = randGen.randString(100) + "@" + "asdfmail.com",
				UserName = randGen.randString(250)
			};
			var userModel = new User
			{
				username = "magentaBro",
				identification_id = identification.Id,
				date_joined = new DateOnly(2020, 02, 20),
				profile_picture = "Red profile picture!"
			};

			_bookShelfContext.Users.Add(identification);
			var result = _userService.AddUser(userModel);

			Assert.True(result.success);
			MappedComparator.CompareUser(userModel, result.payload);
			MappedComparator.CompareUser(_bookShelfContext.User.OrderBy(x => x.pKey).LastOrDefault(), result.payload);
		}
		[Fact]
		public void AddUser_IsUserModelNoUsername_ResultsFailure()
		{
			var errorMessage = "[System.String[]] Username is required!";
			var identification = new Identification()
			{
				Id = randGen.randString(250),
				EmailConfirmed = true,
				LockoutEnabled = false,
				TwoFactorEnabled = false,
				Email = randGen.randString(100) + "@" + "asdfmail.com",
				UserName = randGen.randString(250)
			};
			var userModel = new User
			{
				identification_id = identification.Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			_bookShelfContext.Users.Add(identification);
			var result = _userService.AddUser(userModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddUser_IsUserModelLongUsername_ResultsFailure()
		{
			var errorMessage = "[System.String[]] Name length exceeds 25 characters!";
			var identification = new Identification()
			{
				Id = randGen.randString(250),
				EmailConfirmed = true,
				LockoutEnabled = false,
				TwoFactorEnabled = false,
				Email = randGen.randString(100) + "@" + "asdfmail.com",
				UserName = randGen.randString(250)
			};
			var userModel = new User
			{
				username = "magentaBroaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
				identification_id = identification.Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			_bookShelfContext.Users.Add(identification);
			var result = _userService.AddUser(userModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddUser_IsUserModelNoIdentification_ResultsFailure()
		{
			var errorMessage = "[System.String[]] Identification is needed!";
			var userModel = new User
			{
				username = "magentaBro",
				date_joined = new DateOnly(2020, 02, 20),
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			var result = _userService.AddUser(userModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddUser_IsUserModelNoDate_ResultsFailure()
		{
			var errorMessage = "[System.String[]] Date is required!";
			var identification = new Identification()
			{
				Id = randGen.randString(250),
				EmailConfirmed = true,
				LockoutEnabled = false,
				TwoFactorEnabled = false,
				Email = randGen.randString(100) + "@" + "asdfmail.com",
				UserName = randGen.randString(250)
			};
			var userModel = new User
			{
				username = "magentaBro",
				identification_id = identification.Id,
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			_bookShelfContext.Users.Add(identification);
			var result = _userService.AddUser(userModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddUser_IsUserModelDescription400Characters_ResultsFailure()
		{
			var errorMessage = "[System.String[]] Description length exceeds 300 characters!";
			var identification = new Identification()
			{
				Id = randGen.randString(250),
				EmailConfirmed = true,
				LockoutEnabled = false,
				TwoFactorEnabled = false,
				Email = randGen.randString(100) + "@" + "asdfmail.com",
				UserName = randGen.randString(250)
			};
			var userModel = new User
			{
				username = "magentaBro",
				identification_id = identification.Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = new string('x', 400),
				profile_picture = "Red profile picture!"
			};

			_bookShelfContext.Users.Add(identification);
			var result = _userService.AddUser(userModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
	}
}
