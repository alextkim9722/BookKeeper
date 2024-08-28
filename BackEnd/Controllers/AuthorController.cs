using BackEnd.Model;
using BackEnd.Services;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorService _authorService;

		public AuthorController(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		[HttpPost("AddAuthor/{authorJson}")]
		public string AddAuthor(string authorJson)
		{
			var author = JsonConvert.DeserializeObject<Author>(authorJson);
			var addResult = _authorService.AddAuthor(author);
			return JsonConvert.SerializeObject(addResult);
		}
		[HttpGet("GetAuthorById/{id}")]
		public string GetAuthorById(int id)
		{
			var authorResult = _authorService.GetAuthorById(id);
			return JsonConvert.SerializeObject(authorResult);
		}
		[HttpGet("GetAuthorByFirstName/{first}")]
		public string GetAuthorByFirstName(string first)
		{
			var authorsResult = _authorService.GetAuthorByFirstName(first);
			return JsonConvert.SerializeObject(authorsResult);
		}
		[HttpGet("GetAuthorByMiddleName/{middle}")]
		public string GetAuthorByMiddleName(string middle)
		{
			var authorsResult = _authorService.GetAuthorByMiddleName(middle);
			return JsonConvert.SerializeObject(authorsResult);
		}
		[HttpGet("GetAuthorByLastName/{last}")]
		public string GetAuthorByLastName(string last)
		{
			var authorsResult = _authorService.GetAuthorByLastName(last);
			return JsonConvert.SerializeObject(authorsResult);
		}
		[HttpPut("UpdateAuthor/{authorJson}")]
		public string UpdateAuthor(string authorJson)
		{
			var author = JsonConvert.DeserializeObject<Author>(authorJson);
			var addResult = _authorService.UpdateAuthor(author.pKey, author);
			return JsonConvert.SerializeObject(addResult);
		}
		[HttpDelete("DeleteAuthor/{authorId}")]
		public string DeleteAuthor(int authorId)
		{
			var deleteResult = _authorService.RemoveAuthor([authorId]);
			return JsonConvert.SerializeObject(deleteResult);
		}
	}
}
