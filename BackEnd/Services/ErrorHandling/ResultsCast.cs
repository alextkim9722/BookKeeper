namespace BackEnd.Services.ErrorHandling
{
    public class ResultsCast<T, U> : Results<T>
    {
        public ResultsCast(Results<U> result, T? payload)
        {
            success = result.success;
            this.payload = payload;
            msg = result.msg;
        }
    }
}
