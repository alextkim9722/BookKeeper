using BackEnd.Services.Context;
using BackEnd.Services.ErrorHandling;
using BackEnd.Services.Validate;
using BackEnd.Services.Generics.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace BackEnd.Services.Generics
{
	public class GenericService<T> : IGenericService<T> where T : class
	{
		private readonly BookShelfContext _bookShelfContext;
		public GenericService(BookShelfContext bookShelfContext)
		{
			_bookShelfContext = bookShelfContext;
		}

		public Results<T> AddModel(T model)
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

		public Results<T> ProcessUniqueModel(Expression<Func<T, bool>> condition, Func<T, Results<T>>? processFunction = null)
		{
			var result = ProcessModels(condition,processFunction);
			return new ResultsCast<T, IEnumerable<T>>(result, result.payload?.FirstOrDefault());
		}

		public Results<IEnumerable<T>> ProcessModels(Expression<Func<T, bool>> condition, Func<T, Results<T>>? processFunction = null)
		{
			var models = _bookShelfContext.Set<T>().Where(condition).ToList();

			if (!models.IsNullOrEmpty())
			{
				var results = Process(models, processFunction);
				return results;
			}
			else
			{
				return new ResultsFailure<IEnumerable<T>>("Models were not found!");
			}
		}

		public Results<T> UpdateModel(T updatedModel, int firstKey, int? secondKey = null)
		{
			T model = null;
			if (secondKey == null)
			{
				model = _bookShelfContext.Set<T>().Find(firstKey);
			}
			else
			{
				model = _bookShelfContext.Set<T>().Find(firstKey, secondKey);
			}

			if (model != null)
			{
				var result = Validation.validateModel(updatedModel);

				if (result.success)
				{
					_bookShelfContext.Entry(model).CurrentValues.SetValues(updatedModel);
					_bookShelfContext.SaveChanges();
				}

				return result;
			}
			else
			{
				return new ResultsFailure<T>("Could not find model!");
			}
		}

		public Results<IEnumerable<T>> DeleteModels(IEnumerable<int[]> idList, Func<T, Results<T>>? processFunction = null)
		{
			var models = GetModelsWithIds(idList);

			if (!models.IsNullOrEmpty())
			{
				using (var transaction = _bookShelfContext.Database.BeginTransaction())
				{
					try
					{
						var results = Process(models, processFunction);

						_bookShelfContext.Set<T>().RemoveRange(models);
						_bookShelfContext.SaveChanges();

						transaction.Commit();
						return new ResultsSuccessful<IEnumerable<T>>(models);
					}
					catch (SqlException sqlEx)
					{
						transaction.Rollback();
						return new ResultsFailure<IEnumerable<T>>(sqlEx.Message);
					}
				}
			}
			else
			{
				return new ResultsFailure<IEnumerable<T>>("No models to delete were found!");
			}

		}

		private IEnumerable<T> GetModelsWithIds(IEnumerable<int[]> idList)
		{
			List<T> models = new List<T>();

			foreach (var id in idList)
			{
				T? model = null;
				if (id.Count() == 1)
				{
					model = _bookShelfContext.Set<T>().Find(id[0]);
				}
				else
				{
					model = _bookShelfContext.Set<T>().Find(id[0], id[1]);
				}

				if(model != null)
				{
					models.Add(model);
				}
			}

			return models;
		}

		private Results<IEnumerable<T>> Process(IEnumerable<T> models, Func<T, Results<T>>? processFunction = null)
		{
			if (processFunction != null)
			{
				foreach (var model in models)
				{
					var result = processFunction(model);
					if (!result.success)
					{
						return new ResultsFailure<IEnumerable<T>>("Processing has failed!");
					}
				}
			}

			return new ResultsSuccessful<IEnumerable<T>>(models);
		}
	}
}
