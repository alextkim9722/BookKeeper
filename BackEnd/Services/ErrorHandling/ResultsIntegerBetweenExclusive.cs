namespace BackEnd.Services.ErrorHandling
{
	public class ResultsIntegerBetweenExclusive<T> : Results<T>
	{
		public ResultsIntegerBetweenExclusive(
			T? payload, int min, int max, int input)
		{
			if (isWithinExclusive(min, max, input))
			{
				if (payload == null)
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
					$"Value is not within range of {min} and {max} exclusive!");
			}
		}

		private bool isWithinExclusive(int min, int max, int input)
			=> (input > min && input < max);
	}
}
