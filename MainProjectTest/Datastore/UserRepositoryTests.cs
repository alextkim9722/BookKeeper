using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Builder;
using System.Data;

using MainProjectTest.Datastore;
using MainProject.Datastore;

namespace MainProjectTest.Datastore
{
    public class UserRepositoryTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly ITestOutputHelper output;

        public UserRepositoryTests(TestDatabaseFixture fixture) => Fixture = fixture;
        public TestDatabaseFixture Fixture { get; }

        [Fact]
        public void GET_USER_ID_1()
        {
            using var context = Fixture.createContext();
            var repository = new UserRepository(context);

            var userID = repository.getUserById(1).id;

            Assert.Equal(1, userID);
        }

        [Theory]
        [InlineData(1, "Alberta123")]
		[InlineData(2, "MarcusF29")]
		public void GET_USERNAME_AFTER_GRABBING_BY_ID(int id, string expected)
		{
			using var context = Fixture.createContext();
			var repository = new UserRepository(context);

			var username = repository.getUserById(id).name;

			Assert.Equal(expected, username);
		}
	}
}
