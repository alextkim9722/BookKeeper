using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using BackEnd.Services.ErrorHandling;
using BackEnd.Model;
using System.Reflection.Metadata.Ecma335;
using Microsoft.IdentityModel.Tokens;

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

        protected Results<IEnumerable<T>> getModelsBy(Expression<Func<T, bool>> condition)
        {
			try
			{
				IEnumerable<T>? model = _bookShelfContext.Set<T>().Where(condition);
                if (!model.IsNullOrEmpty())
                {
					return new ResultsSuccessful<IEnumerable<T>>(model!);
				}else
                {
                    return new ResultsFailure<IEnumerable<T>>("Failed to find models");
                }
			}
			catch (SqlException sqlEx)
			{
				return new ResultsException<IEnumerable<T>>(sqlEx, "Issue with grabbing models");
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
			var models = getModelsBy(condition);

            if(models.success)
            {
                try
                {
                    var model = models.payload!.FirstOrDefault()!;
                    var validation = validateProperties(model);

                    if (validation.success)
                    {
                        _bookShelfContext
                            .Entry(model)
                            .CurrentValues
                            .SetValues(updatedModel);
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
                    return new ResultsException<T>(sqlEx, "Issue with updating model");
                }
            }
            else
            {
                return new ResultsFailure<T>("Model was not found for updating");
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
			var models = getModelsBy(condition);
            if(models.success)
            {
                try
                {
					var model = models.payload!.FirstOrDefault()!;

					_bookShelfContext.Set<T>().Remove(model);
                    _bookShelfContext.SaveChanges();
                    return new ResultsSuccessful<T>(model);
                }
                catch (SqlException sqlEx)
                {
                    return new ResultsException<T>(
                        sqlEx, "Issue with deleting one model");
                }
            }
            else
            {
                return new ResultsFailure<T>("Models were not found for deleting");
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

			if (!CallbackHandler.IsNullOrEmpty())
            {
                result = executeCallbackChain(model);
			}

            return result;
		}

		protected Results<IEnumerable<T>> formatModels(Expression<Func<T, bool>> condition)
		{
            Results<IEnumerable<T>> model = getModelsBy(condition);
			
            if(model.success && !CallbackHandler.IsNullOrEmpty())
            {
                for(int i = 0;i < model.payload!.Count();i++)
                {
                    var result = executeCallbackChain(model.payload!.ToList()[i]);
                    if(result.success)
                    {
                        model.payload!.ToList()[i] = result.payload!;
                    }
                    else
                    {
                        return new ResultsFailure<IEnumerable<T>>(result.msg);
                    }
                }
            }

            return model;
		}

		protected Results<T> formatModel(Expression<Func<T, bool>> condition)
        {
			Results<IEnumerable<T>> model = formatModels(condition);
            if (model.success)
            {
                return new ResultsSuccessful<T>(model.payload!.FirstOrDefault());
            }

            return new ResultsFailure<T>(model.msg);
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
