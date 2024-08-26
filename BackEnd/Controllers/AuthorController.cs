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
			var authorResult = JsonConvert.DeserializeObject<Author>(authorJson);
			var addResult = _authorService.AddAuthor(authorResult);

			return addResult.success ? "success" : addResult.msg;
		}
		[HttpGet("GetAuthorById/{id}")]
		public string GetAuthorById(int id)
		{
			var authorResult = _authorService.GetAuthorById(id);
			if (!authorResult.success) return authorResult.msg;

			return JsonConvert.SerializeObject(authorResult.payload);
		}
		[HttpGet("GetAuthorByFirstName/{first}")]
		public string GetAuthorByFirstName(string first)
		{
			var authorsResult = _authorService.GetAuthorByFirstName(first);
			if (!authorsResult.success) return authorsResult.msg;

			return JsonConvert.SerializeObject(authorsResult.payload);
		}
		[HttpGet("GetAuthorByMiddleName/{middle}")]
		public string GetAuthorByMiddleName(string middle)
		{
			var authorsResult = _authorService.GetAuthorByMiddleName(middle);
			if (!authorsResult.success) return authorsResult.msg;

			return JsonConvert.SerializeObject(authorsResult.payload);
		}
		[HttpGet("GetAuthorByLastName/{last}")]
		public string GetAuthorByLastName(string last)
		{
			var authorsResult = _authorService.GetAuthorByLastName(last);
			if (!authorsResult.success) return authorsResult.msg;

			return JsonConvert.SerializeObject(authorsResult.payload);
		}
		[HttpPut("UpdateAuthor/{authorJson}")]
		public string UpdateAuthor(string authorJson)
		{
			var authorResult = JsonConvert.DeserializeObject<Author>(authorJson);
			var addResult = _authorService.UpdateAuthor(authorResult.pKey, authorResult);

			return addResult.success ? "success" : addResult.msg;
		}
		[HttpDelete("DeleteAuthor/{authorId}")]
		public string DeleteAuthor(int authorId)
		{
			var deleteResult = _authorService.RemoveAuthor([authorId]);
			return deleteResult.success ? "success" : deleteResult.msg;
		}
	}
}
