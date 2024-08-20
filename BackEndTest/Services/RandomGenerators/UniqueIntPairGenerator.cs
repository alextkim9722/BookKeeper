using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.RandomGenerators
{
    public class UniqueIntPairGenerator
    {
        private List<int[]> pairList;
        private readonly RandomGenerators _randomGenerators;

        public UniqueIntPairGenerator(RandomGenerators randomGenerators, int rangeCol, int rangeRow)
        {
            pairList = new List<int[]>();
            _randomGenerators = randomGenerators;

            for (int i = 1; i < rangeCol; i++)
            {
                for (int j = 1; j < rangeRow; j++)
                {
                    pairList.Add(new int[] { i, j });
                }
            }
        }

        public int[] getRandomPair()
        {
            var result = pairList.ElementAt(_randomGenerators.randNumber(0, pairList.Count()));
            pairList.Remove(result);
            return result;
        }
    }
}
