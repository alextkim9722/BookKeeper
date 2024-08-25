using BackEnd.Services.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.DatabaseGenerators.DBConnector
{
	public interface IDatabaseManager
	{
		public BookShelfContext CreateContext();
		public void ClearTables(BookShelfContext bookShelfContext);
		public void ReseedTables(BookShelfContext bookShelfContext);

	}
}
