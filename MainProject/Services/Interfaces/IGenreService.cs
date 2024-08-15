using MainProject.Model;

namespace MainProject.Services.Interfaces
{
	public interface IGenreService
	{
		public Genre createGenreModel(int id);
		public IEnumerable<Genre> createGenreModelBatch(int id);
	}
}
