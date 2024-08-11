using Xunit.Abstractions;

using MainProject.Datastore;

namespace MainProjectTest.Datastore
{
    public class UserRepositoryTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly ITestOutputHelper output;
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
	}
}
