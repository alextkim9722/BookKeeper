namespace BackEnd.ErrorHandling
{
    public class ResultsSuccessful<T> : Results<T>
    {
        public ResultsSuccessful(T? payload)
        {
            successfulResult(payload);
        }
    }
}
