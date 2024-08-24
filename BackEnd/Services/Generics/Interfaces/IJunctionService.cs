using BackEnd.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace BackEnd.Services.Generics.Interfaces
{
	public interface IJunctionService<T> where T : class, IDoublePKModel
	{
		public IEnumerable<int> GetJunctionedJoinedModelsId(int id, bool first);
		public IEnumerable<T> DeleteJunctionModels(int id, bool first);
	}
}
