using MainProject.Datastore;
using MainProject.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Services
{
	public class TableServiceAbstract
	{
		protected readonly BookShelfContext _bookShelfContext;
		protected readonly string _tableName;

		protected TableServiceAbstract(BookShelfContext bookShelfContext, string tableName)
		{
			_bookShelfContext = bookShelfContext;
			_tableName = tableName;
		}

		protected T? getModelBy<T>(string field, string value)
		{
			T? model = default(T);

			try
			{
				model = _bookShelfContext.Database.SqlQuery<T>(
					$"select * from [dbo].[{_tableName}] where {field} = {value}").First();
				return model;
			}
			catch (SqlException sqlEx)
			{
				return default(T);
			}
		}
	}
}
