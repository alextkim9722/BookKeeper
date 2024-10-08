﻿using BackEnd.Model.Interfaces;
using BackEnd.Services.Context;
using BackEnd.Services.Generics.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Services.Generics
{
	public class JunctionService<T> : IJunctionService<T> where T : class, IDoublePKModel
	{
		private readonly BookShelfContext _bookShelfContext;

		public JunctionService(
			BookShelfContext bookShelfContext)
		{
			_bookShelfContext = bookShelfContext;
		}

		public IEnumerable<T> GetJunctionModels(int id, bool first)
			=> _bookShelfContext.Set<T>().Where(x => first ? x.firstKey == id : x.secondKey == id);

		public IEnumerable<int> GetJunctionedJoinedModelsId(int id, bool first)
		{
			var junctionResults = GetJunctionModels(id, first);
			IEnumerable<int> keys = new List<int>();

			if (!junctionResults.IsNullOrEmpty())
			{
				keys = junctionResults.Select(x => first ? x.secondKey : x.firstKey).ToList();
			}

			return keys;
		}

		public IEnumerable<T> DeleteJunctionModels(int id, bool first)
		{
			var models = GetJunctionModels(id, first);
			_bookShelfContext.Set<T>().RemoveRange(models);
			_bookShelfContext.SaveChanges();
			return models;
		}
	}
}
