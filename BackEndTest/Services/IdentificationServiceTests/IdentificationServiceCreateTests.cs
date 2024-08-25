using BackEnd.Services.Context;
using BackEnd.Services;
using BackEndTest.Services.DatabaseGenerators;
using BackEndTest.Services.RandomGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Model;
using BackEnd.Services.Generics;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moq;
using BackEnd.Services.ErrorHandling;
using BackEndTest.Services.DatabaseGenerators.DBConnector;
using BackEndTest.Services.Comparator;

namespace BackEndTest.Services.IdentificationServiceTests
{
	/*
	 * We're using a unit test for the Identification Service for the sake of ease.
	 * Compared to the other services, Identification Service does not need a full integration
	 * test as the UserService is thoroughly tested previously.
	 * 
	 * As a result, we can get away with doing a moq test for this service.
	 */
	[Collection("Service Tests")]
	public class IdentificationServiceCreateTests : IClassFixture<IdentificationDatabaseGenerator>
	{
		private readonly ValueGenerators _randGen = new ValueGenerators();
		private readonly IdentificationService _identificationService;
		private readonly BookShelfContext _bookShelfContext;

		private readonly Mock<UserManager<Identification>> _userManager;
		private readonly Mock<IUserService> _userService;

		public IdentificationServiceCreateTests(IdentificationDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_userManager = new Mock<UserManager<Identification>>(new UserStore<Identification>(_bookShelfContext), null, null, null, null, null, null, null, null);
			_userService = new Mock<IUserService>();
			_identificationService = new IdentificationService(_userManager.Object, _userService.Object);
		}

		[Fact]
		public void CreateIdentification_IsIdentificationModelPasswordYellowPassword_ResultsSuccessful()
		{
			var password = "YellowPassword";
			var identification = new Identification()
			{
				Id = _randGen.randString(250),
				EmailConfirmed = true,
				LockoutEnabled = false,
				TwoFactorEnabled = false,
				Email = "RedEmail@TheMail.com",
				UserName = "RedAccount"
			};
			_userManager.Setup(x => x.CreateAsync(identification, password)).ReturnsAsync(IdentityResult.Success);
			// Although the return payload is null, it won't be an issue during creation
			_userService.Setup(x => x.AddUser(It.IsAny<User>())).Returns(new ResultsSuccessful<User>(null));

			var result = _identificationService.createIdentification(identification, password);

			Assert.True(result.success);
		}

		[Fact]
		public void CreateIdentification_IsNullAndYellowPassword_ResultsFailure()
		{
			var errorMessage = "Identification is null";
			var password = "YellowPassword";
			_userManager.Setup(x => x.CreateAsync(null, password)).ReturnsAsync(IdentityResult.Failed());

			var result = _identificationService.createIdentification(null, password);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void CreateIdentification_IsIdentificationModelAndYellowPasswordWithFailedMoq_ResultsFailure()
		{
			var password = "YellowPassword";
			var identification = new Identification()
			{
				Id = _randGen.randString(250),
				EmailConfirmed = true,
				LockoutEnabled = false,
				TwoFactorEnabled = false,
				Email = "RedEmail@TheMail.com",
				UserName = "RedAccount"
			};
			_userManager.Setup(x => x.CreateAsync(identification, password)).ReturnsAsync(IdentityResult.Failed());

			var result = _identificationService.createIdentification(identification, password);

			Assert.False(result.success);
		}
		[Fact]
		public void CreateIdentification_IsIdentificationModelAndYellowPasswordWithFailedUserMoq_ResultsFailure()
		{
			var errorMessage = "[ERROR]: Failed user\r\n";
			var password = "YellowPassword";
			var identification = new Identification()
			{
				Id = _randGen.randString(250),
				EmailConfirmed = true,
				LockoutEnabled = false,
				TwoFactorEnabled = false,
				Email = "RedEmail@TheMail.com",
				UserName = "RedAccount"
			};
			_userManager.Setup(x => x.CreateAsync(identification, password)).ReturnsAsync(IdentityResult.Success);
			// Although the return payload is null, it won't be an issue during creation
			_userService.Setup(x => x.AddUser(It.IsAny<User>())).Returns(new ResultsFailure<User>("Failed user"));

			var result = _identificationService.createIdentification(identification, password);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
	}
}
