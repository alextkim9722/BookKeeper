using BackEnd.Model;
using BackEnd.Repository.Interfaces;
using BackEnd.ErrorHandling;

namespace BackEnd.Services.Abstract
{
    public abstract class AbstractService<T> where T : class
    {
        private readonly ITableService<T> _tableService;
        private readonly IKeyBatchService<T> _keyBatchService;

        public AbstractService(
            ITableService<T> tableService,
            IKeyBatchService<T> keyBatchService)
        {
            _tableService = tableService;
            _keyBatchService = keyBatchService;
        }

        protected Results<T> AddModel(T model)
            => _tableService.addModel(model);
    }
}
