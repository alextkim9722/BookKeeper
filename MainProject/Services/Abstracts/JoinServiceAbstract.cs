using MainProject.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net;

namespace MainProject.Services.Abstracts
{
    public abstract class JoinServiceAbstract<T> : TableServiceAbstract<T> where T : class
    {
        protected JoinServiceAbstract(BookShelfContext bookShelfContext)
            : base(bookShelfContext)
        { }

        protected bool deleteJoins<U>(Expression<Func<U, bool>> condition)
            where U : class
        {
            try
            {
                _bookShelfContext.Set<U>().RemoveRange(
                    _bookShelfContext.Set<U>().Where(condition));
                _bookShelfContext.SaveChanges();

                return true;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                return false;
            }
        }

        protected IEnumerable<U>? getJoins<U>(Expression<Func<U, bool>> condition)
            where U : class
        {
            try
            {
                return _bookShelfContext.Set<U>().Where(condition).ToList();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                return null;
            }
        }

        protected IEnumerable<U>? getMultipleJoins<U, V>(
            Expression<Func<V, bool>> bridgeCondition,
            Expression<Func<V, int>> foreignKey
            )
            where U : class
            where V : class
        {
            try
            {
                var bridges = getJoins(bridgeCondition);

                if (bridges != null)
                {
                    var bridgeKeys = bridges.AsQueryable().Select(foreignKey).ToList();
                    List<U>? targets = new List<U>();
                    foreach (var key in bridgeKeys)
                    {
                        var foreignModel = _bookShelfContext.Set<U>().Find(key);
                        if (foreignModel != null)
                        {
                            targets.Add(foreignModel);
                        }
                    }

                    return targets;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                return null;
            }
        }

        protected abstract bool deleteBridges(int id);

        protected abstract T addBridges(T model);
    }
}
