using BackEnd.Model;
using BackEnd.Services;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IBookService _bookService;

		public UserController(IUserService userService, IBookService bookService)
		{
			_userService = userService;
			_bookService = bookService;
		}

		[HttpPost("AddUser/{userJson}")]
		public string AddUser(string userJson)
		{
			var user = JsonConvert.DeserializeObject<User>(userJson);
			var addResult = _userService.AddUser(user);

			return JsonConvert.SerializeObject(user);
		}
		[HttpGet("GetUserById/{id}")]
		public string GetUserById(int id)
		{
			var userResult = _userService.GetUserById(id);
			if (userResult.success)
			{
				var booksResult = _bookService.GetBooksByUser(id);
				if (!booksResult.success) return JsonConvert.SerializeObject(booksResult);

				var ratingResult = _userService.SetBooksAndPagesRead(userResult.payload, booksResult.payload);
				if (!ratingResult.success) return JsonConvert.SerializeObject(ratingResult);
			}

			return JsonConvert.SerializeObject(userResult);
		}
		[HttpGet("GetUserByIdentificationId/{id}")]
		public string GetUserByIdentificationId(string id)
		{
			var userResult = _userService.GetUserByIdentificationId(id);
			if (userResult.success)
			{
				var booksResult = _bookService.GetBooksByUser(userResult.payload.pKey);
				if (!booksResult.success) return JsonConvert.SerializeObject(booksResult);

				var ratingResult = _userService.SetBooksAndPagesRead(userResult.payload, booksResult.payload);
				if (!ratingResult.success) return JsonConvert.SerializeObject(ratingResult);
			}

			return JsonConvert.SerializeObject(userResult);
		}
		[HttpGet("GetUserByUserName/{username}")]
		public string GetUserByUserName(string username)
		{
			var userResult = _userService.GetUserByUserName(username);
			if (userResult.success)
			{
				foreach (var user in userResult.payload)
				{
					var booksResult = _bookService.GetBooksByUser(user.pKey);
					if (!booksResult.success) return JsonConvert.SerializeObject(booksResult);

					var ratingResult = _userService.SetBooksAndPagesRead(user, booksResult.payload);
					if (!ratingResult.success) return JsonConvert.SerializeObject(ratingResult);
				}
			}

			return JsonConvert.SerializeObject(userResult);
		}
		[HttpGet("GetUsersByBook/{id}")]
		public string GetUsersByBook(int id)
		{
			var usersResult = _userService.GetUsersByBook(id);
			return JsonConvert.SerializeObject(usersResult);
		}
		[HttpPut("UpdateUser/{bookJson}")]
		public string UpdateUser(string userJson)
		{
			var user = JsonConvert.DeserializeObject<User>(userJson);
			var updateResult = _userService.UpdateUser(user.pKey, user);

			return JsonConvert.SerializeObject(updateResult);
		}
		[HttpDelete("DeleteUser/{userId}")]
		public string DeleteUser(int userId)
		{
			var deleteResult = _userService.RemoveUser([userId]);
			return JsonConvert.SerializeObject(deleteResult);
		}
	}
}
