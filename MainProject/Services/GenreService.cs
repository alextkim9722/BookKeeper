using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.Services.Interfaces;

namespace MainProject.Services
{
	public class GenreService : IGenreService
	{
		private readonly IGenreRepository _genreRepository;

		public GenreService(IGenreRepository genreRepository)
			=> _genreRepository = genreRepository;

		public GenreModel createGenreModel(int id)
			=> _genreRepository.getGenreById(id);

		public IEnumerable<GenreModel> createGenreModelBatch(int bookId)
			=> _genreRepository.getGenreByBook(bookId);
	}
}
