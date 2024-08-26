using BackEnd.Model;
using BackEnd.Services.ErrorHandling;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services.Interfaces
{
	public interface IRoleService
	{
		public Results<IdentityRole> CreateRole(string name);
		public Results<IdentityRole> DeleteRole(string name);
		public Results<IdentityRole> AddToRole(string identificationId, string name);
		public Results<IdentityRole> RemoveFromRole(string identificationId, string name);
	}
}
