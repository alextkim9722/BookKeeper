using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
	public class RandomGenerators
	{
		private const string allowedCharacters = "abcdefghijklmnopqrstuvwxyz1234567890";
		private readonly Random _random;

		public RandomGenerators() {
			_random = new Random();
		}

		public int randNumber(int min, int max)
		{
			if(min >= max)
			{
				return 0;
			}
			else
			{

				return Math.Abs(_random.Next(min, max));
			}
		}

		public string randString(int max)
		{
			string result = "";

			for(int i = 0;i < max;i++)
			{
				result += allowedCharacters[_random.Next(0,allowedCharacters.Count())];
			}

			return result;
		}

		public DateOnly randDate()
		{
			var start = new DateOnly(2000, 1, 1);
			return start.AddDays(randNumber(0, 1000));
		}
	}
}
