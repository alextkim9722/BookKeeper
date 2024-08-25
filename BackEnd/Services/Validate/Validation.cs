using BackEnd.Services.ErrorHandling;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Services.Validate
{
    public static class Validation
    {
        public static Results<T> validateModel<T>(T model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (validationResults.IsNullOrEmpty())
            {
                return new ResultsSuccessful<T>(model);
            }
            else
            {
                return new ResultsFailure<T>(validationResults);
            }
        }
    }
}
