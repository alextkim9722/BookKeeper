namespace BackEnd.Services.ErrorHandling
{
	public class ResultsException<T> : ResultsFailure<T>
	{
		public ResultsException(Exception exception, string msg)
			: base(msg)
		{
			failedResult($"[EXCEPTION] {exception}");
		}
	}
}
