using BackEnd.Model;
using BackEnd.Services.Context;
using BackEndTest.Services.DatabaseGenerators.DBConnector;
using BackEndTest.Services.DatabaseGenerators.Interfaces;
using BackEndTest.Services.RandomGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.DatabaseGenerators
{
	public class UserDatabaseGenerator : IDatabaseGenerator
	{
		private IDatabaseManager databaseManager = new DatabaseManager();
		private ValueGenerators randGen = new ValueGenerators();
		private bool databaseCreated = false;

		public UserDatabaseGenerator()
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
			var userBooks = new User_Book[]
			{
				new User_Book()
				{
					firstKey = 3,
					secondKey = 1
				},
				new User_Book()
				{
					firstKey = 2,
					secondKey = 1
				},
				new User_Book()
				{
					firstKey = 1,
					secondKey = 2
				},
				new User_Book()
				{
					firstKey = 1,
					secondKey = 1
				}
			};
			bookShelfContext.User_Book.AddRange(userBooks);

			for (var i = 0; i < userBooks.Count(); i++)
			{
				var review = new Review()
				{
					firstKey = userBooks[i].firstKey,
					secondKey = userBooks[i].secondKey,
					date_submitted = randGen.randDate(),
					description = randGen.randString(200),
					rating = 2 * i
				};

				bookShelfContext.Review.Add(review);
			}

			bookShelfContext.SaveChanges();
		}

		public void PopulateTables(BookShelfContext bookShelfContext)
		{
			var identificationTemp = new List<Identification>();

			for (int i = 0; i < 3; i++)
			{
				Identification identification = new Identification()
				{
					Id = randGen.randString(250),
					EmailConfirmed = true,
					LockoutEnabled = false,
					TwoFactorEnabled = false,
					Email = randGen.randString(100) + "@" + "asdfmail.com",
					UserName = randGen.randString(250)
				};
				Book book = new Book()
				{
					title = randGen.randString(25),
					pages = (i + 1)*5,
					isbn = randGen.randString(13),
					cover_picture = randGen.randString(25)
				};

				identificationTemp.Add(identification);
				bookShelfContext.Users.Add(identification);
				bookShelfContext.Book.Add(book);
			}

			identificationTemp = identificationTemp.OrderBy(x => x.Id).ToList();

			var users = new User[]
			{
				new User()
				{
					username = "redBro",
					identification_id = identificationTemp[0].Id,
					date_joined = new DateOnly(2020,02,20),
					description = "The red description!",
					profile_picture = "Red profile picture!"
				},
				new User()
				{
					username = "greenBro",
					identification_id = identificationTemp[1].Id,
					date_joined = new DateOnly(2010,12,10),
					description = "The green description!",
					profile_picture = "Green profile picture!"
				},
				new User()
				{
					username = "blueBro",
					identification_id = identificationTemp[2].Id,
					date_joined = new DateOnly(2000,04,12),
					description = "The blue description!",
					profile_picture = "Blue profile picture!"
				}
			};
			bookShelfContext.User.AddRange(users);

			bookShelfContext.SaveChanges();
		}
	}
}
