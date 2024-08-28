using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.RandomGenerators
{
	public class PopulateTable : IClassFixture<TestDatabaseGenerator>
	{
		public PopulateTable(TestDatabaseGenerator generator) { }

		[Fact]
		public void Generate()
		{
			Assert.True(true);
		}
	}
}
