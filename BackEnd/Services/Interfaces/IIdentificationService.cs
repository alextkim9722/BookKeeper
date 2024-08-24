using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services.Interfaces
{
    public interface IIdentificationService
	{
		public Task<Results<Identification>> createIdentification(
			Identification identification, string password);
		public Task<Results<Identification>> updateIdentification(
			Identification identification);
		public Task<Results<Identification>> removeIdentification(string id);
		public Task<Results<Identification>> getIdentificationByUsername(
			string username);
		public Task<Results<Identification>> getIdentificationByEmail(string email);
	}
}
