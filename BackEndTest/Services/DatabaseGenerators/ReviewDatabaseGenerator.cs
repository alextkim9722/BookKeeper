using BackEnd.Model;
using BackEndTest.Services.DatabaseGenerators.DBConnector;
using BackEndTest.Services.RandomGenerators;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Services.Context;
using BackEndTest.Services.DatabaseGenerators.Interfaces;

namespace BackEndTest.Services.DatabaseGenerators
{
	public class ReviewDatabaseGenerator : IDatabaseGenerator
	{
		private IDatabaseManager databaseManager = new DatabaseManager();
		private ValueGenerators randGen = new ValueGenerators();
		private bool databaseCreated = false;

		public ReviewDatabaseGenerator()
		{
			if (!databaseCreated)
			{
				var context = databaseManager.CreateContext();
				databaseManager.ClearTables(context);
				databaseManager.ReseedTables(context);
				PopulateTables(context);
				PopulateBridgeTables(context);
				databaseCreated = true;
			}
		}

		public BookShelfContext GetContext() => databaseManager.CreateContext();

		public void PopulateBridgeTables(BookShelfContext bookShelfContext)
		{
			var reviews = new Review[]
			{
				new Review()
				{
					firstKey = 3,
					secondKey = 1,
					date_submitted = new DateOnly(2020,02,22),
					description = "red description!",
					rating = 4
				},
				new Review()
				{
					firstKey = 1,
					secondKey = 1,
					date_submitted = new DateOnly(2010,01,11),
					description = "blue description!",
					rating = 2
				},
				new Review()
				{
					firstKey = 2,
					secondKey = 2,
					date_submitted = new DateOnly(2023,03,12),
					description = "green description!",
					rating = 9
				},
				new Review()
				{
					firstKey = 2,
					secondKey = 3,
					date_submitted = new DateOnly(2000,06,01),
					description = "yellow description!",
					rating = 8
				}
			};

			bookShelfContext.Review.AddRange(reviews);
			bookShelfContext.SaveChanges();
		}

		public void PopulateTables(BookShelfContext bookShelfContext)
		{
			for (int i = 0; i < 3; i++)
			{
				Identification identification = new Identification()
				{
					EmailConfirmed = true,
					LockoutEnabled = false,
					TwoFactorEnabled = false,
					Email = randGen.randString(100) + "@" + "asdfmail.com",
					UserName = randGen.randString(250)
				};
				User user = new User()
				{
					username = randGen.randString(10),
					identification_id = identification.Id,
					date_joined = randGen.randDate(),
					description = randGen.randString(25),
					profile_picture = randGen.randString(25)
				};
				Book book = new Book()
				{
					title = randGen.randString(25),
					pages = randGen.randNumber(1, 1000),
					isbn = randGen.randString(13),
					cover_picture = randGen.randString(25)
				};

				bookShelfContext.Users.Add(identification);
				bookShelfContext.User.Add(user);
				bookShelfContext.Book.Add(book);
			}

			bookShelfContext.SaveChanges();
		}
	}
}
