using BackEnd.ErrorHandling;
using System.Linq.Expressions;

namespace BackEnd.Repository.Interfaces
{
    public interface IKeyBatchService<T> where T : class
    {
        public Results<IEnumerable<T>> deleteModels(Expression<Func<T, bool>> condition);
        public Results<IEnumerable<T>> getModels(Expression<Func<T, bool>> condition);
    }
}
