using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using BackEnd.Services.ErrorHandling;
using BackEnd.Model;

namespace BackEnd.Services.Abstracts
{
    public abstract class TableServiceAbstract<T> where T : class
    {
        protected readonly BookShelfContext _bookShelfContext;
		protected delegate Results<T> Callback(T model);
		protected List<Callback> CallbackHandler;

		protected TableServiceAbstract(BookShelfContext bookShelfContext)
        {
            _bookShelfContext = bookShelfContext;
            CallbackHandler = new List<Callback>();
        }

        protected Results<T> getModelBy(Expression<Func<T, bool>> condition)
        {
            try
            {
                T? model = _bookShelfContext.Set<T>().Where(condition).FirstOrDefault();
                return new ResultsSuccessful<T>(model!);
            }
            catch (SqlException sqlEx)
            {
                return new ResultsException<T>(sqlEx, "Issue with grabbing model");
            }
        }

        protected Results<IEnumerable<T>> getAllModels()
        {
            try
            {
				IEnumerable<T>? models = _bookShelfContext.Set<T>().ToList();
                return new ResultsSuccessful<IEnumerable<T>>(models);
            }
            catch (SqlException sqlEx)
            {
				return new ResultsException<IEnumerable<T>>(
                    sqlEx, "Issue with getting all models");
			}
        }

        protected Results<T> updateModelMapped(
            Expression<Func<T, bool>> condition,
            T updatedModel)
        {
			var model = getModelBy(condition);

            if(model.success)
            {
                try
                {
                    var validation = validateProperties(model.payload!);
                    if (validation.success)
                    {
                        _bookShelfContext
                            .Entry(model!)
                            .CurrentValues
                            .SetValues(updatedModel);
                        _bookShelfContext.SaveChanges();

                        return new ResultsSuccessful<T>(model.payload!);
                    }
                    else
                    {
                        return validation;
                    }
                }
                catch (SqlException sqlEx)
                {
                    return new ResultsException<T>(sqlEx, "Issue with updating model");
                }
            }
            else
            {
                return model;
            }
        }

        protected Results<T> addModel(T model)
        {
            try
            {
                var validation = validateProperties(model);
                if (validation.success)
                {
                    _bookShelfContext.Set<T>().Add(model);
                    _bookShelfContext.SaveChanges();
                    return new ResultsSuccessful<T>(model);
                }
                else
                {
                    return validation;
                }
            }
            catch (SqlException sqlEx)
            {
                return new ResultsException<T>(sqlEx, "Issue with adding one model");
            }
        }

        protected Results<T> deleteModel(
            Expression<Func<T, bool>> condition)
        {
			var model = getModelBy(condition);
            if(model.success)
            {
                try
                {
                    _bookShelfContext.Set<T>().Remove(model.payload!);
                    _bookShelfContext.SaveChanges();
                    return new ResultsSuccessful<T>(model.payload!);
                }
                catch (SqlException sqlEx)
                {
                    return new ResultsException<T>(
                        sqlEx, "Issue with deleting one model");
                }
            }
            else
            {
                return model;
            }
        }

        protected Results<T> updateModel(
			Expression<Func<T, bool>> condition,
			T updated)
        {
			Results<T> original = updateModelMapped(condition, updated);

			if (original.success)
			{
                original.payload = transferProperties(original.payload!, updated);
                return original;
			}
			else
			{
				return new ResultsFailure<T>(
					original.msg
					+ "Failed to update model");
			}
		}

        protected Results<T> formatModel(T model)
		{
            Results<T> result = new Results<T>();

			if (CallbackHandler != null)
            {
                result = executeCallbackChain(model);
			}

            return result;
		}

        protected Results<T> formatModel(Expression<Func<T, bool>> condition)
        {
			Results<T> model = getModelBy(condition);

            if(model.success && CallbackHandler != null)
            {
                model = formatModel(model.payload!);
            }

            return model;
		}

		public Results<IEnumerable<T>> formatAllModels()
		{
			var models = getAllModels();
            Results<T> result = new Results<T>();

			if (models.success)
			{
                var modelList = models.payload!.ToList();

                for(int i = 0;i < modelList.Count();i++)
                {
                    result = formatModel(modelList[i]);
                    if(!result.success)
                    {
                        return new ResultsFailure<IEnumerable<T>>(
                            "Failure to format model");
                    }
                }

                return models;
			}

            return models;
		}

        private Results<T> executeCallbackChain(T model)
        {
            if(model != null)
            {
                foreach (var callback in CallbackHandler)
                {
                    Results<T> current = callback.Invoke(model);
                    if(!current.success)
                    {
                        // If one callback fails then the chain must stop.
                        return current;
                    }
                }

                return new ResultsSuccessful<T>(model);
            }
            else
            {
                return new ResultsFailure<T>(
                    "Callback input object is null");
            }
        }

        protected abstract T transferProperties(T original, T updated);

        protected abstract Results<T> validateProperties(T model);
	}
}
