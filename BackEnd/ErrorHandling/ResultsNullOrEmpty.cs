﻿using Microsoft.IdentityModel.Tokens;

namespace BackEnd.ErrorHandling
{
    public class ResultsNullOrEmpty<T> : Results<IEnumerable<T>>
    {
        public ResultsNullOrEmpty(IEnumerable<T> models, string msg)
        {
            if (!models.IsNullOrEmpty())
            {
                successfulResult(models);
            }
            else
            {
                failedResult(msg);
            }
        }
    }
}
