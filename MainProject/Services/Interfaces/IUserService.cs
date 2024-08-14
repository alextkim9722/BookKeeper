using MainProject.Model;

namespace MainProject.Services.Interfaces
{
	public interface IUserService
	{
		public UserModel getUserModelById(int id);
	}
}
