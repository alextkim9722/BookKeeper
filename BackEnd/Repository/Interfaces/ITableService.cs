using BackEnd.ErrorHandling;
using System.Linq.Expressions;

namespace BackEnd.Repository.Interfaces
{
    public interface ITableService<T> where T : class
    {
        public Results<T> addModel(T model);
        public Results<T> getUniqueModel(Expression<Func<T, bool>> condition);
        public Results<IEnumerable<T>> getAllModels();
        public Results<T> updateModel(Expression<Func<T, bool>> condition, T updatedModel);
    }
}
