using Microsoft.Data.SqlClient;
using BackEnd.Services.ErrorHandling;
using System.Linq.Expressions;
using System.Collections.Generic;
using BackEnd.Model;

namespace BackEnd.Services.Abstracts
{
    public abstract class JoinService<T> where T : class
    {
        private readonly BookShelfContext _bookShelfContext;

        protected JoinService(BookShelfContext bookShelfContext)
        {
            _bookShelfContext = bookShelfContext;
        }

        protected Results<T> deleteJoins<U>(Expression<Func<U, bool>> condition) where U : class
        {
            try
            {
                _bookShelfContext.Set<U>().RemoveRange(
                    _bookShelfContext.Set<U>().Where(condition));
                _bookShelfContext.SaveChanges();

                return new ResultsSuccessful<T>(null);
            }
            catch (SqlException sqlEx)
            {
                return new ResultsException<T>(
                    sqlEx, "Failed to delete joins");
            }
        }

        protected Results<IEnumerable<U>> getJoins<U>(
            Expression<Func<U, bool>> condition)
            where U : class
        {
            try
            {
                var joins = _bookShelfContext
                    .Set<U>()
                    .Where(condition)
                    .ToList();
				return new ResultsSuccessful<IEnumerable<U>>(joins);
            }
            catch (SqlException sqlEx)
            {
                return new ResultsException<IEnumerable<U>>(
                    sqlEx, "Failed to get joins");
            }
        }

        protected Results<T> deleteBridgedModel(int id, Expression<Func<T, bool>> condition)
        {
			Results<T> deletingBridges = deleteBridges(id);

			if (deletingBridges.success)
			{
				return deleteModel(condition);
			}
			else
			{
				return new ResultsFailure<T>(
					deletingBridges.msg
					+ "Failed to delete joins");
			}
		}

        protected Results<IEnumerable<U>> getMultipleJoins<U, V>(
            Expression<Func<V, bool>> bridgeCondition,
            Expression<Func<V, int>> foreignKey
            )
            where U : class
            where V : class
        {
            var bridges = getJoins(bridgeCondition);

            if (bridges.success)
            {
                var bridgeKeys = bridges.payload!
                    .AsQueryable().Select(foreignKey).ToList();

                if (bridgeKeys != null)
                {
                    List<U>? targets = new List<U>();

                    foreach (var key in bridgeKeys)
                    {
                        var foreignModel = _bookShelfContext
                            .Set<U>().Find(key);
                        if (foreignModel != null)
                        {
                            targets.Add(foreignModel);
                        }
                    }

                    return new ResultsSuccessful<IEnumerable<U>>(targets);
                }
                else
                {
                    return new ResultsFailure<IEnumerable<U>>(
                        "Failed to select foreign key for bridge table");
                }
            }
            else
            {
                return new ResultsFailure<IEnumerable<U>>(bridges.msg);
            }
        }
    }
}
