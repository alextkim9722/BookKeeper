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
using BackEnd.Services.Interfaces;

namespace BackEndTest.Services.UserServiceTests
{
	[Collection("Service Tests")]
	public class UserServiceDeleteTests : IClassFixture<UserDatabaseGenerator>
	{
		private readonly UserService _userService;
		private readonly BookShelfContext _bookShelfContext;
		private ValueGenerators randGen = new ValueGenerators();

		public UserServiceDeleteTests(UserDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_userService = new UserService(
				new GenericService<User>(_bookShelfContext),
				new JunctionService<User_Book>(_bookShelfContext),
				new JunctionService<Review>(_bookShelfContext));
		}

		[Fact]
		public void RemoveUser_Is1_ResultsSuccessfulFindResultsFailure()
		{
			var id = 1;

			var result = _userService.RemoveUser([id]);

			Assert.True(result.success);
			Assert.Null(_bookShelfContext.User.Find(id));
		}
	}
}
