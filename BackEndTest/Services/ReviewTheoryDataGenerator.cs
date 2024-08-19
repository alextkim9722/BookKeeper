using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
	public class ReviewTheoryDataGenerator : TheoryData<Review>
	{
		public ReviewTheoryDataGenerator()
		{
			for (int i = 0; i < 20; i++)
			{
				Review review = TestDatabaseGenerator.reviewTable[i];

				Add(review);
			}
		}
	}
}
