using MainProject.ViewModels;
using MainProject.Model;

namespace MainProject.Controllers.UseCases
{
	public interface IShelfPageGet
	{
		public ShelfPageViewModel createViewModel(int id);
	}
}
