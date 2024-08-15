using MainProject.Datastore;
using MainProject.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace MainProject.Services
{
	public class TableServiceAbstract<T> where T : class
	{
		protected readonly BookShelfContext _bookShelfContext;
		protected readonly string _tableName;

		protected TableServiceAbstract(BookShelfContext bookShelfContext, string tableName)
		{
			_bookShelfContext = bookShelfContext;
			_tableName = tableName;
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

		protected T? updateModel(int id, T updatedModel)
		{
			T? model = null;

			try
			{
				model = _bookShelfContext.Set<T>().Find(id);
				_bookShelfContext.Entry(model!).CurrentValues.SetValues(updatedModel);
				_bookShelfContext.SaveChanges();

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
	}
}
