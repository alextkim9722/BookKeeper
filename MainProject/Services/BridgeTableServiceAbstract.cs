using MainProject.Datastore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MainProject.Services
{
	public abstract class BridgeTableServiceAbstract : TableServiceAbstract
	{
		protected readonly string _primaryKeyName;
		protected BridgeTableServiceAbstract(BookShelfContext bookShelfContext, String primaryKeyName, string tableName)
			: base(bookShelfContext, tableName)
		{
			_primaryKeyName = primaryKeyName;
		}

		protected bool deleteBridgeTableConnection(string bridgeName, int id)
		{
			try
			{
				_bookShelfContext.Database.ExecuteSql(
					$"delete from [dbo].[{bridgeName}] where {_primaryKeyName} = {id}");

				return true;
			}catch(SqlException sqlEx)
			{
				Console.WriteLine(sqlEx.Message);
				return false;
			}
		}
		protected IEnumerable<TTarget> getBridgeTableConnections<TTarget>(
			string bridgeName, string tableName, int id, string targetKey)
		{
			var bridgeList = _bookShelfContext.Database.SqlQuery<int>(
				$"select {targetKey} from [dbo].[{bridgeName}] where {_primaryKeyName} = {id}");

			return _bookShelfContext.Database.SqlQuery<TTarget>(
				$"select * from [dbo].[{tableName}] where {targetKey} in {bridgeList}")
				.ToList();
		}
	}
}
