using Xunit;
using MainProjectTests.Datastore;

namespace MainProject.Datastore.Tests
{
	public class UserRepositoryTests : IClassFixture<TestDatabaseFixture>
	{
		public UserRepositoryTests(TestDatabaseFixture fixture) => Fixture = fixture;
		public TestDatabaseFixture Fixture { get; }

		[Fact]
		public void GetUser()
		{
			using var context = Fixture.createContext();
			var repository = new UserRepository(context);

			var userID = repository.getUserById(1).id;

			Assert.Equal(1, userID);
		}
	}
}