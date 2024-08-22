using BackEnd.ErrorHandling;
using System.Linq.Expressions;

namespace BackEnd.Repository.Interfaces
{
    public interface IJunctionService<T, U>
        where T : class
        where U : class
    {
        public Results<IEnumerable<T>> GetJunctionedJoin(Expression<Func<U, bool>> junctionCondition,Expression<Func<U, int>> foreignKey);
    }
}
