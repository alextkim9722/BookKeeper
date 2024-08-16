using BackEnd.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace BackEnd.Services.Abstracts
{
    public class TableServiceAbstract<T> where T : class
    {
        protected readonly BookShelfContext _bookShelfContext;
		protected delegate T? Callback(T model);
		protected Callback? CallbackHandler;

		protected TableServiceAbstract(BookShelfContext bookShelfContext)
        {
            _bookShelfContext = bookShelfContext;
        }

        protected T? getModelBy(Expression<Func<T, bool>> condition)
        {
            T? model = null;

            try
            {
                model = _bookShelfContext.Set<T>().Where(condition).FirstOrDefault();
                return model;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                return null;
            }
        }

        protected IEnumerable<T>? getAllModels()
        {
            IEnumerable<T>? model = null;

            try
            {
                model = _bookShelfContext.Set<T>().ToList();
                return model;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                return null;
            }
        }

        protected T? updateModel(
            Expression<Func<T, bool>> condition,
            T updatedModel)
        {
            T? model = null;

            try
            {
                model = _bookShelfContext
                    .Set<T>()
                    .Where(condition)
                    .FirstOrDefault();
                _bookShelfContext
                    .Entry(model!)
                    .CurrentValues
                    .SetValues(updatedModel);
                _bookShelfContext
                    .SaveChanges();

                return model;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                return null;
            }
        }

        protected T? addModel(T model)
        {
            try
            {
                _bookShelfContext.Set<T>().Add(model);
                _bookShelfContext.SaveChanges();
                return model;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                return null;
            }
        }

        protected T? deleteModel(Expression<Func<T, bool>> condition)
        {
            T? model = null;

            try
            {
                model = _bookShelfContext.Set<T>().Where(condition).First();
                _bookShelfContext.Set<T>().Remove(model);
                _bookShelfContext.SaveChanges();
                return model;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                return null;
            }
        }

		public IEnumerable<T>? formatAllModels()
		{
			var models = getAllModels().ToList();
			if (models != null)
			{
                for(int i = 0;i < models.Count();i++)
                {
                    models[i] = formatModel(models[i], null);
                }

				return models;
			}
			else
			{
				return null;
			}
		}
        protected T? formatModel(
			T? model = null,
			Expression<Func<T, bool>>? condition = null)
		{
            if (condition != null)
            {
				model = getModelBy(condition);
			}			

			if (model != null && CallbackHandler != null)
			{
				model = CallbackHandler(model);
			}

			return model;
		}
	}
}
