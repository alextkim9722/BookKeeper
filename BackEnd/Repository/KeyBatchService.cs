using BackEnd.ErrorHandling;
using BackEnd.Repository.Interfaces;
using BackEnd.Services;
using System.Linq.Expressions;

namespace BackEnd.Repository
{
    public class KeyBatchService<T> : IKeyBatchService<T> where T : class
    {
        private readonly BookShelfContext _bookShelfContext;
        public KeyBatchService(BookShelfContext bookShelfContext)
        {
            _bookShelfContext = bookShelfContext;
        }
        public Results<IEnumerable<T>> getModels(Expression<Func<T, bool>> condition)
        {
            var models = _bookShelfContext.Set<T>().Where(condition);
            return new ResultsNullOrEmpty<T>(models, "Models are not found");
        }

        public Results<IEnumerable<T>> deleteModels(Expression<Func<T, bool>> condition)
        {
            var modelsResult = getModels(condition);
            if (modelsResult.success)
            {
                _bookShelfContext.Set<T>().RemoveRange(modelsResult.payload!);
                _bookShelfContext.SaveChanges();
            }

            return modelsResult;
        }
    }
}
