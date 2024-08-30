using BackEnd.Model;
using BackEnd.Services;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly IGenreService _genreService;

		public GenreController(IGenreService genreService)
		{
			_genreService = genreService;
		}

		[HttpPost("AddGenre/{genreJson}")]
		public string AddGenre(string genreJson)
		{
			var genre = JsonConvert.DeserializeObject<Genre>(genreJson);
			var addResult = _genreService.AddGenre(genre);
			return JsonConvert.SerializeObject(addResult);
		}
		[HttpGet("GetGenreById/{id}")]
		public string GetGenreById(int id)
		{
			var genre = _genreService.GetGenreById(id);
			return JsonConvert.SerializeObject(genre);
		}
		[HttpGet("GetGenreByName/{name}")]
		public string GetGenreByName(string name)
		{
			var genres = _genreService.GetGenreByName(name);
			return JsonConvert.SerializeObject(genres);
		}
		[HttpGet("GetGenreByBook/{id}")]
		public string GetGenreByBook(int id)
		{
			var genresResult = _genreService.GetGenreByBook(id);
			return JsonConvert.SerializeObject(genresResult);
		}
		[HttpPut("UpdateGenre/{genreJson}")]
		public string UpdateGenre(string genreJson)
		{
			var genre = JsonConvert.DeserializeObject<Genre>(genreJson);
			var updateResult = _genreService.UpdateGenre(genre.pKey, genre);

			return JsonConvert.SerializeObject(updateResult);
		}
		[HttpDelete("DeleteGenre/{genreId}")]
		public string DeleteGenre(int genreId)
		{
			var deleteResult = _genreService.RemoveGenre([genreId]);
			return JsonConvert.SerializeObject(deleteResult);
		}
	}
}
