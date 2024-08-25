using BackEnd.Model;
using BackEnd.Services.ErrorHandling;
using BackEnd.Services.Generics.Interfaces;
using BackEnd.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace BackEnd.Services
{
	public class GenreService : IGenreService
	{
		private readonly IGenericService<Genre> _genericService;
		private readonly IJunctionService<Book_Genre> _jBookGenreService;

		public GenreService(IGenericService<Genre> genericService,
			IJunctionService<Book_Genre> jBookGenreService)
		{
			_genericService = genericService;
			_jBookGenreService = jBookGenreService;
		}

		public Results<Genre> AddGenre(Genre genre)
			=> _genericService.AddModel(genre);
		public Results<Genre> GetGenreById(int id)
			=> _genericService.ProcessUniqueModel(x => x.pKey == id, AddDependents);
		public Results<IEnumerable<Genre>> GetGenreByName(string name)
			=> _genericService.ProcessModels(x => x.genre_name == name, AddDependents);
		public Results<Genre> UpdateGenre(int id, Genre genre)
			=> _genericService.UpdateModel(genre, id);
		public Results<IEnumerable<Genre>> RemoveGenre(IEnumerable<int> id)
			=> _genericService.DeleteModels([id.ToArray()], DeleteDependents);

		public Results<Genre> AddDependents(Genre genre)
		{
			try
			{
				genre.books = _jBookGenreService.GetJunctionedJoinedModelsId(genre.pKey, false);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<Genre>("Failed to add bridges");
			}

			return new ResultsSuccessful<Genre>(genre);
		}
		private Results<Genre> DeleteDependents(Genre genre)
		{
			try
			{
				_jBookGenreService.DeleteJunctionModels(genre.pKey, false);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<Genre>("Failed to add bridges");
			}

			return new ResultsSuccessful<Genre>(genre);
		}
	}
}
