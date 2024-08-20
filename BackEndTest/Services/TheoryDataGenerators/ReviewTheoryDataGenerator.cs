using BackEnd.Model;
using BackEndTest.Services.RandomGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.TheoryDataGenerators
{
    public class ReviewTheoryDataGenerator : TheoryData<Review>
    {
        public ReviewTheoryDataGenerator()
        {
            for (int i = 0; i < TestDatabaseGenerator._bridgeTableValueCount; i++)
            {
                Review review = TestDatabaseGenerator.reviewTable[i];

                Add(review);
            }
        }
    }
}
