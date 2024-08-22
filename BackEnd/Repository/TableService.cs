using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;
using BackEnd.ErrorHandling;
using BackEnd.Services;
using BackEnd.Repository.Interfaces;

namespace BackEnd.Repository
{
    public class TableService<T> : ITableService<T> where T : class
    {
        private readonly BookShelfContext _bookShelfContext;

        protected TableService(BookShelfContext bookShelfContext)
        {
            _bookShelfContext = bookShelfContext;
        }

        public Results<T> addModel(T model)
        {
            var result = Validation.validateModel(model);

            if (result.success)
            {
                _bookShelfContext.Set<T>().Add(model);
                _bookShelfContext.SaveChanges();
                return new ResultsSuccessful<T>(model);
            }
            else
            {
                return result;
            }
        }

        public Results<T> getUniqueModel(Expression<Func<T, bool>> condition)
        {
            var result = _bookShelfContext.Set<T>().Where(condition).FirstOrDefault();
            if (result != null)
            {
                return new ResultsSuccessful<T>(result);
            }
            else
            {
                return new ResultsFailure<T>("Failure to find model!");
            }
        }

        public Results<IEnumerable<T>> getAllModels()
        {
            var models = _bookShelfContext.Set<T>().ToList();
            return new ResultsNullOrEmpty<T>(models, "Table is empty!");
        }

        public Results<T> updateModel(Expression<Func<T, bool>> condition, T updatedModel)
        {
            var model = getUniqueModel(condition);

            if (model.success)
            {
                var result = Validation.validateModel(model.payload);

                if (result.success)
                {
                    foreach (var property in typeof(T).GetProperties())
                    {
                        property.SetValue(model.payload, updatedModel);
                    }
                }

                return result!;
            }
            else
            {
                return model;
            }
        }
    }
}
