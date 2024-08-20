using BackEnd.Model;
using BackEndTest.Services.RandomGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.TheoryDataGenerators
{
	public class IdentificationTheoryDataGenerator : TheoryData<Identification>
	{
		public IdentificationTheoryDataGenerator()
		{
			for (int i = 0; i < TestDatabaseGenerator._tableValueCount; i++)
			{
				Identification identification = TestDatabaseGenerator.identificationTable[i];

				identification.user_id = TestDatabaseGenerator.userTable
					.Where(x => x.user_id == i).FirstOrDefault()!.user_id;

				Add(identification);
			}
		}
	}
}
