using MainProject.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProjectTest.Datastore
{
	// TEST DOES NOT AFFECT DATABASE ONLY READS IT
	public class AuthorRepositoryTest : IClassFixture<TestDatabaseFixture>
	{
		private BookShelfContext context;
		private AuthorRepository repository;
		public TestDatabaseFixture Fixture;

		public AuthorRepositoryTest(TestDatabaseFixture fixture)
		{
			Fixture = fixture;
			context = Fixture.createContext();
			repository = new AuthorRepository(context);
		}

		[Fact]
		public void GET_AUTHORS_FOR_BOOK_INDEX_1()
		{
			int bookid = 1;
			string expectedfirstname = "Nicole";

			string actualname = repository.getAuthorByBook(bookid).FirstOrDefault().first_name;

			Assert.Equal(expectedfirstname, actualname);
		}

		[Fact]
		public void GET_AUTHOR_COUNT_FOR_BOOK_INDEX_4()
		{
			int bookid = 4;
			int expectedcount = 2;

			int actualname = repository.getAuthorByBook(bookid).ToList().Count;

			Assert.Equal(expectedcount, actualname);
		}
	}
}
