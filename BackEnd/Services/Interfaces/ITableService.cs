using BackEnd.Services.ErrorHandling;
using System.Linq.Expressions;

namespace BackEnd.Services.Interfaces
{
    public interface ITableService<T> where T : class
    {
        public Results<T> addModel(T model);
        public Results<T> getModel(Expression<Func<T, bool>> condition);
        public Results<IEnumerable<T>> getModels(Expression<Func<T, bool>> condition);
        public Results<IEnumerable<T>> getAllModels();
        public Results<T> updateModel(Expression<Func<T, bool>> condition, T updatedModel);
        public Results<IEnumerable<T>> deleteModels(Expression<Func<T, bool>> condition);
    }
}
