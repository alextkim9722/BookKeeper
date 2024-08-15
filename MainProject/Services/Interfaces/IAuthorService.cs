using MainProject.ViewModel;
using MainProject.Model;

namespace MainProject.Services.Interfaces
{
	public interface IAuthorService
	{
		public Author createAuthorModel(int id);
		public IEnumerable<Author> createAuthorModelBatch(int bookId);
		public void createAuthorModelBatch(IEnumerable<Author> authorModelList);
	}
}
