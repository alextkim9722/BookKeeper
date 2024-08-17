using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
	public interface IIdentificationService
	{
		public Task<Identification>? createIdentification(
			Identification identification, string password);
		public Task<Identification>? updateIdentification(
			string id, Identification identification);
		public Task<Identification>? removeIdentification(string id);
		public Task<Identification>? getIdentificationByUsername(
			string username);
		public Task<Identification>? getIdentificationByEmail(string email);
	}
}
