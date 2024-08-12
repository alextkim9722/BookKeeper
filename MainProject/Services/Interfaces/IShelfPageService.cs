using MainProject.ViewModel;
using MainProject.Model;

namespace MainProject.Services.Interfaces
{
    public interface IShelfPageService
    {
        public ShelfPageViewModel createViewModel(int id);
    }
}
