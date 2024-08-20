using BackEnd.Model;
using BackEnd.Services.ErrorHandling;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services.Interfaces
{
	public interface IRoleService
	{
		public Task<Results<IdentityRole>> createRole(string name);
		public Task<Results<IdentityRole>> deleteRole(string name);
		public Task<Results<IdentityRole>> addToRole(User user, string name);
		public Task<Results<IdentityRole>> removeFromRole(User user, string name);
	}
}
