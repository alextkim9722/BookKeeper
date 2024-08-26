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
			var genreResult = JsonConvert.DeserializeObject<Genre>(genreJson);
			var addResult = _genreService.AddGenre(genreResult);

			return addResult.success ? "success" : addResult.msg;
		}
		[HttpGet("GetGenreById/{id}")]
		public string GetGenreById(int id)
		{
			var genreResult = _genreService.GetGenreById(id);
			if (!genreResult.success) return genreResult.msg;

			return JsonConvert.SerializeObject(genreResult.payload);
		}
		[HttpGet("GetGenreByName/{name}")]
		public string GetGenreByName(string name)
		{
			var genresResult = _genreService.GetGenreByName(name);
			if (!genresResult.success) return genresResult.msg;

			return JsonConvert.SerializeObject(genresResult.payload);
		}
		[HttpPut("UpdateGenre/{genreJson}")]
		public string UpdateGenre(string genreJson)
		{
			var genreResult = JsonConvert.DeserializeObject<Genre>(genreJson);
			var addResult = _genreService.UpdateGenre(genreResult.pKey, genreResult);

			return addResult.success ? "success" : addResult.msg;
		}
		[HttpDelete("DeleteGenre/{genreId}")]
		public string DeleteGenre(int genreId)
		{
			var deleteResult = _genreService.RemoveGenre([genreId]);
			return deleteResult.success ? "success" : deleteResult.msg;
		}
	}
}
