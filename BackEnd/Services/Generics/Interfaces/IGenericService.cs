using BackEnd.Services.ErrorHandling;
using System.Linq.Expressions;

namespace BackEnd.Services.Generics.Interfaces
{
	public interface IGenericService<T> where T : class
	{   
		public Results<T> AddModel(T model);
		public Results<T> ProcessUniqueModel(Expression<Func<T, bool>> condition, Func<T, Results<T>>? processFunction = null);
		public Results<IEnumerable<T>> ProcessModels(Expression<Func<T, bool>> condition, Func<T, Results<T>>? processFunction = null);
		public Results<T> UpdateModel(T updatedModel, int firstKey, int? secondKey = null);
		public Results<IEnumerable<T>> DeleteModels(IEnumerable<int[]> idList, Func<T, Results<T>>? processFunction = null);
	}
}
