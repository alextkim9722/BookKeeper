using System.Linq.Expressions;
using BackEnd.Services.ErrorHandling;
using Microsoft.IdentityModel.Tokens;
using BackEnd.Services.Interfaces;
using BackEnd.Services.Modifiers.Interfaces;

namespace BackEnd.Services
{
    public class TableService<T> : ITableService<T> where T : class
    {
        private readonly BookShelfContext _bookShelfContext;

        protected TableService(BookShelfContext bookShelfContext, IModifiers<T> modifier)
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

        public Results<T> getModel(Expression<Func<T, bool>> condition)
        {
            var result = getModels(condition);
            if (result.success)
            {
                return new ResultsSuccessful<T>(result.payload!.FirstOrDefault());
            }
            else
            {
                return new ResultsFailure<T>(result.msg);
            }
        }

        public Results<IEnumerable<T>> getModels(Expression<Func<T, bool>> condition)
        {
            var models = _bookShelfContext.Set<T>().Where(condition);
            return resultsFound(models);
        }

        public Results<IEnumerable<T>> getAllModels()
        {
            var models = _bookShelfContext.Set<T>().ToList();
            return resultsFound(models);
        }

        public Results<T> updateModel(Expression<Func<T, bool>> condition, T updatedModel)
        {
            var model = getModel(condition);

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

        private Results<IEnumerable<T>> resultsFound(IEnumerable<T>? models)
        {
            if (!models.IsNullOrEmpty())
            {
                return new ResultsSuccessful<IEnumerable<T>>(models);
            }
            else
            {
                return new ResultsFailure<IEnumerable<T>>("Entries not found!");
            }
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
