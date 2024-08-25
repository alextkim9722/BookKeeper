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
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace BackEndTest.Services.UserServiceTests
{
	[Collection("Service Tests")]
	public class UserServiceUpdateTests : IClassFixture<UserDatabaseGenerator>
	{
		private readonly UserService _userService;
		private readonly BookShelfContext _bookShelfContext;
		private ValueGenerators randGen = new ValueGenerators();

		public UserServiceUpdateTests(UserDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_userService = new UserService(
				new GenericService<User>(_bookShelfContext),
				new JunctionService<User_Book>(_bookShelfContext),
				new JunctionService<Review>(_bookShelfContext));
		}

		[Fact]
		public void UpdateUser_IsUserModel1NoUsername_ResultsFailure()
		{
			var id = 1;
			var errorMessage = "[System.String[]] Username is required!";
			var userModel = new User
			{
				pKey = id,
				identification_id = _bookShelfContext.Users.OrderBy(x => x.Id).FirstOrDefault().Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			var result = _userService.AddUser(userModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddUser_IsUserModel1LongUsername_ResultsFailure()
		{
			var id = 1;
			var errorMessage = "[System.String[]] Name length exceeds 25 characters!";
			var userModel = new User
			{
				pKey = id,
				username = "magentaBroaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
				identification_id = _bookShelfContext.Users.OrderBy(x => x.Id).FirstOrDefault().Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			var result = _userService.AddUser(userModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddUser_IsUserModelNoIdentification_ResultsFailure()
		{
			var id = 1;
			var errorMessage = "[System.String[]] Identification is needed!";
			var userModel = new User
			{
				pKey = id,
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
			var id = 1;
			var errorMessage = "[System.String[]] Date is required!";
			var userModel = new User
			{
				pKey = id,
				username = "magentaBro",
				identification_id = _bookShelfContext.Users.OrderBy(x => x.Id).FirstOrDefault().Id,
				description = "The red description!",
				profile_picture = "Red profile picture!"
			};

			var result = _userService.AddUser(userModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddUser_IsUserModelDescription400Characters_ResultsFailure()
		{
			var id = 1;
			var errorMessage = "[System.String[]] Description length exceeds 300 characters!";
			var userModel = new User
			{
				pKey = id,
				username = "magentaBro",
				identification_id = _bookShelfContext.Users.OrderBy(x => x.Id).FirstOrDefault().Id,
				date_joined = new DateOnly(2020, 02, 20),
				description = new string('x', 400),
				profile_picture = "Red profile picture!"
			};

			var result = _userService.AddUser(userModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
	}
}
