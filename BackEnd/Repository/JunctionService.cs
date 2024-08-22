using System.Linq.Expressions;
using BackEnd.ErrorHandling;
using BackEnd.Services;
using BackEnd.Repository.Interfaces;

namespace BackEnd.Repository
{
    public class JunctionService<T, U> : IJunctionService<T, U>
        where T : class
        where U : class
    {
        private readonly BookShelfContext _bookShelfContext;
        private readonly IKeyBatchService<T> _tableService;
        private readonly IKeyBatchService<U> _junctionService;

        public JunctionService(
            BookShelfContext bookShelfContext,
            IKeyBatchService<T> tableService,
            IKeyBatchService<U> junctionService)
        {
            _bookShelfContext = bookShelfContext;
            _tableService = tableService;
            _junctionService = junctionService;
        }

        public Results<IEnumerable<T>> GetJunctionedJoin(
            Expression<Func<U, bool>> junctionCondition,
            Expression<Func<U, int>> foreignKey
            )
        {
            var junctionResult = _junctionService.getModels(junctionCondition);

            if (junctionResult.success)
            {
                var bridgeKeys = junctionResult.payload!.AsQueryable().Select(foreignKey).ToList();

                List<T> targets = new List<T>();

                foreach (var key in bridgeKeys)
                {
                    var foreignModel = _bookShelfContext.Set<T>().Find(key);
                    if (foreignModel != null)
                    {
                        targets.Add(foreignModel);
                    }
                }

                return new ResultsSuccessful<IEnumerable<T>>(targets);
            }
            else
            {
                return new ResultsFailure<IEnumerable<T>>(junctionResult.msg);
            }
        }
    }
}
