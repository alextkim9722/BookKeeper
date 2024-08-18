namespace BackEnd.Services.ErrorHandling
{
	public class ResultsFailure<T> : Results<T>
	{
		public ResultsFailure(string msg)
		{
			failedResult($"{msg}");
		}
	}
}
