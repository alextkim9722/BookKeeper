using System.Transactions;

namespace BackEnd.Services.ErrorHandling
{
	public class ResultsIntegerBetweenInclusive<T> : Results<T> 
	{
		public ResultsIntegerBetweenInclusive(
			T? payload, int min, int max, int input)
		{
			if(isWithinInclusive(min, max, input))
			{
				if(payload == null)
				{
					failedResult("Payload value is null!");
				}
				else
				{
					successfulResult(payload);
				}
			}
			else
			{
				failedResult(
					$"Value is not within range of {min} and {max} inclusive!");
			}
		}

		private bool isWithinInclusive(int min, int max, int input)
			=> (input >= min && input <= max);
	}
}
