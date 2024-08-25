using BackEnd.Model;
using BackEnd.Services.Context;
using BackEndTest.Services.DatabaseGenerators.DBConnector;
using BackEndTest.Services.DatabaseGenerators.Interfaces;
using BackEndTest.Services.RandomGenerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.DatabaseGenerators
{
	public class IdentificationDatabaseGenerator
	{
		private IDatabaseManager databaseManager = new DatabaseManager();
		private static UserManager<Identification> _userManager;
		private ValueGenerators randGen = new ValueGenerators();
		private bool databaseCreated = false;

		public IdentificationDatabaseGenerator()
		{
			if (!databaseCreated)
			{
				var context = databaseManager.CreateContext();
				databaseManager.ClearTables(context);
				databaseManager.ReseedTables(context);
				databaseCreated = true;
			}
		}

		public BookShelfContext GetContext() => databaseManager.CreateContext();
		public UserManager<Identification> GetUserManager() => _userManager;

		/*
		public void PopulateBridgeTables(BookShelfContext bookShelfContext)
		{
			throw new NotImplementedException();
		}

		public void PopulateTables(BookShelfContext bookShelfContext)
		{
			UserManager<Identification> userManager = new UserManager<Identification>(new UserStore<Identification>(databaseManager.CreateContext()), null, null, null, null, null, null, null, null);

			var identifications = new Identification[]
			{
				new Identification()
				{
					Id = randGen.randString(250),
					AccessFailedCount = 0,
					EmailConfirmed = true,
					LockoutEnabled = false,
					PhoneNumberConfirmed = false,
					TwoFactorEnabled = false,
					Email = "RedEmail@TheMail.com",
					UserName = "RedAccount"
				},
				new Identification()
				{
					Id = randGen.randString(250),
					AccessFailedCount = 0,
					EmailConfirmed = true,
					LockoutEnabled = false,
					PhoneNumberConfirmed = false,
					TwoFactorEnabled = false,
					Email = "GreenEmail@TheMail.com",
					UserName = "GreenAccount"
				},
				new Identification()
				{
					Id = randGen.randString(250),
					AccessFailedCount = 0,
					EmailConfirmed = true,
					LockoutEnabled = false,
					PhoneNumberConfirmed = false,
					TwoFactorEnabled = false,
					Email = "BlueEmail@TheMail.com",
					UserName = "BlueAccount"
				}
			};
			Task.Run(() => userManager.CreateAsync(identifications[0], "RedPassword@123")).GetAwaiter().GetResult();
			Task.Run(() => userManager.CreateAsync(identifications[1], "GreenPassword@123")).GetAwaiter().GetResult();
			Task.Run(() => userManager.CreateAsync(identifications[2], "BluePassword@123")).GetAwaiter().GetResult();
			_userManager = userManager;

			for (int i = 0; i < 3; i++)
			{
				User user = new User()
				{
					username = randGen.randString(10),
					identification_id = identifications[i].Id,
					date_joined = randGen.randDate(),
					description = randGen.randString(25),
					profile_picture = randGen.randString(25)
				};

				bookShelfContext.User.Add(user);
			}

			bookShelfContext.SaveChanges();
		}
		*/
	}
}
