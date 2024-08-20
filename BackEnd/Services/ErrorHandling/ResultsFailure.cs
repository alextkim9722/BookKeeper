using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services.ErrorHandling
{
	public class ResultsFailure<T> : Results<T>
	{
		public ResultsFailure(string msg)
		{
			failedResult($"{msg}");
		}

		public ResultsFailure(IEnumerable<IdentityError> msg)
		{
			foreach (var error in msg)
			{
				failedResult($"[{error.Code}] {error.Description}");
			}
		}
	}
}
