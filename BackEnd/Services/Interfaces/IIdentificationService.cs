using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services.Interfaces
{
	public interface IIdentificationService
	{
		public Results<Identification> createIdentification(
			Identification identification, string password);
		public Results<Identification> updateIdentification(
			Identification identification);
		public Results<Identification> removeIdentification(string id);
		public Results<Identification> getIdentificationByUsername(
			string username);
		public Results<Identification> getIdentificationByEmail(string email);
	}
}
