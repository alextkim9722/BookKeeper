using Xunit.Abstractions;

using MainProject.Datastore;

namespace MainProjectTest.Datastore
{
    public class UserRepositoryTests : IClassFixture<TestDatabaseFixture>
    {
		private BookShelfContext context;
		private UserRepository repository;
		public TestDatabaseFixture Fixture;

		public UserRepositoryTests(TestDatabaseFixture fixture)
		{
			Fixture = fixture;
			context = Fixture.createContext();
			repository = new UserRepository(context);
		}

        [Theory]
        [InlineData(0, "alberto153")]
		[InlineData(1, "bertthebart751")]
		[InlineData(2, "palpal8457")]
		public void GET_USERNAME_AFTER_GRABBING_BY_ID(int id, string expected)
		{
			var username = repository.getUserById(id).username;

			Assert.Equal(expected, username);
		}

		[Theory]
		[InlineData("alberto153", "alberto153")]
		[InlineData("bertthebart751", "bertthebart751")]
		[InlineData("palpal8457", "palpal8457")]
		public void GET_USERNAME_AFTER_GRABBING_BY_USERNAME(string name, string expected)
		{
			var username = repository.getUserByName(name).username;

			Assert.Equal(expected, username);
		}
	}
}
