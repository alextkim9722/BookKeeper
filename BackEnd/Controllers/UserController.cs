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
			var userResult = JsonConvert.DeserializeObject<User>(userJson);
			var addResult = _userService.AddUser(userResult);

			return addResult.success ? "success" : addResult.msg;
		}
		[HttpGet("GetUserById/{id}")]
		public string GetUserById(int id)
		{
			var userResult = _userService.GetUserById(id);
			if (!userResult.success) return userResult.msg;

			var booksResult = _bookService.GetBooksByUser(id);
			if (booksResult.success)
			{
				var ratingResult = _userService.SetBooksAndPagesRead(userResult.payload, booksResult.payload);
				if (!ratingResult.success) return ratingResult.msg;
			}

			return JsonConvert.SerializeObject(userResult.payload);
		}
		[HttpGet("GetUserByIdentificationId/{id}")]
		public string GetUserByIdentificationId(string id)
		{
			var userResult = _userService.GetUserByIdentificationId(id);
			if (!userResult.success) return userResult.msg;

			var booksResult = _bookService.GetBooksByUser(userResult.payload.pKey);
			if (booksResult.success)
			{
				var ratingResult = _userService.SetBooksAndPagesRead(userResult.payload, booksResult.payload);
				if (!ratingResult.success) return ratingResult.msg;
			}

			return JsonConvert.SerializeObject(userResult.payload);
		}
		[HttpGet("GetUserByUserName/{username}")]
		public string GetUserByUserName(string username)
		{
			var userResult = _userService.GetUserByUserName(username);
			if (!userResult.success) return userResult.msg;

			foreach (var user in userResult.payload)
			{
				var booksResult = _bookService.GetBooksByUser(user.pKey);
				if (booksResult.success)
				{
					var ratingResult = _userService.SetBooksAndPagesRead(user, booksResult.payload);
					if (!ratingResult.success) return ratingResult.msg;
				}
			}

			return JsonConvert.SerializeObject(userResult.payload);
		}
		[HttpPut("UpdateUser/{bookJson}")]
		public string UpdateUser(string userJson)
		{
			var userResult = JsonConvert.DeserializeObject<User>(userJson);
			var updateResult = _userService.UpdateUser(userResult.pKey, userResult);

			return updateResult.success ? "success" : updateResult.msg;
		}
		[HttpDelete("DeleteUser/{userId}")]
		public string DeleteUser(int userId)
		{
			var deleteResult = _userService.RemoveUser([userId]);
			return deleteResult.success ? "success" : deleteResult.msg;
		}
	}
}
