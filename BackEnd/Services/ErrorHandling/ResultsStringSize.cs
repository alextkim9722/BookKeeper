namespace BackEnd.Services.ErrorHandling
{
	public class ResultsStringSize<T> : Results<T>
	{
		public ResultsStringSize(T payload, string input, int size)
		{
			if (input.Count() <= size)
			{
				successfulResult(payload);
			}
			else
			{
				failedResult($"String is longer than {size}");
			}
		}
	}
}
