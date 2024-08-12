using MainProject.Model;

namespace MainProject.Services.Interfaces
{
	public interface IGenreService
	{
		public GenreModel createGenreModel(int id);
		public IEnumerable<GenreModel> createGenreModelBatch(int id);
	}
}
