namespace BackEnd.ErrorHandling
{
    public class ResultsCast<T, U> : Results<T>
    {
        public ResultsCast(Results<U> result, T payload) {
            success = result.success;
            msg = result.msg;
        }
    }
}
