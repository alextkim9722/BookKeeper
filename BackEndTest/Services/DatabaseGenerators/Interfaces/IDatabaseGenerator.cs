using BackEnd.Services.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.DatabaseGenerators.Interfaces
{
    public interface IDatabaseGenerator
    {
        public BookShelfContext GetContext();
        public void PopulateTables(BookShelfContext bookShelfContext);
        public void PopulateBridgeTables(BookShelfContext bookShelfContext);
    }
}
