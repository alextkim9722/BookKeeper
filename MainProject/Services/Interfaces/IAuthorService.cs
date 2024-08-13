using MainProject.ViewModel;
using MainProject.Model;

namespace MainProject.Services.Interfaces
{
	public interface IAuthorService
	{
		public AuthorModel createAuthorModel(int id);
		public IEnumerable<AuthorModel> createAuthorModelBatch(int bookId);
		public void createAuthorModelBatch(IEnumerable<AuthorModel> authorModelList);
	}
}
