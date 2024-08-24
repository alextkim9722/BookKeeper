using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Services.ErrorHandling
{
    public class ResultsFailure<T> : Results<T>
    {
        public ResultsFailure(string msg)
        {
            failedResult($"{msg}");
        }

        public ResultsFailure(Exception exception, string msg)
        {
            failedResult($"[EXCEPTION] {exception}");
        }

        public ResultsFailure(IEnumerable<IdentityError> msg)
        {
            foreach (var error in msg)
            {
                failedResult($"[{error.Code}] {error.Description}");
            }
        }

        public ResultsFailure(IEnumerable<ValidationResult> msg)
        {
            foreach (var error in msg)
            {
                failedResult($"[{error.MemberNames}] {error.ErrorMessage}");
            }
        }
    }
}
