using BackEnd.Services;
using BackEnd.Services.Interfaces;
using BackEnd.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookService _bookService;
		private readonly IReviewService _reviewService;

		public BookController(IBookService bookService, IReviewService reviewService)
		{
			_bookService = bookService;
			_reviewService = reviewService;
		}

		[HttpPost("AddBook/{bookJson}")]
		public string AddBook(string bookJson)
		{
			var bookResult = JsonConvert.DeserializeObject<Book>(bookJson);
			var addResult = _bookService.AddBook(bookResult);

			return addResult.success ? "success" : addResult.msg;
		}
		[HttpGet("GetBookById/{id}")]
		public string GetBookById(int id)
		{
			var bookResult = _bookService.GetBookById(id);
			if (!bookResult.success) return bookResult.msg;

			var reviews = _reviewService.GetReviewByBookId(bookResult.payload.pKey);
			if (reviews.success)
			{
				var ratingResult = _bookService.SetRating(bookResult.payload, reviews.payload);
				if (!ratingResult.success) return ratingResult.msg;
			}

			return JsonConvert.SerializeObject(bookResult.payload);
		}
		[HttpGet("GetBookByIsbn/{isbn}")]
		public string GetBookByIsbn(string isbn)
		{
			var bookResult = _bookService.GetBookByIsbn(isbn);
			if (!bookResult.success) return bookResult.msg;

			var reviews = _reviewService.GetReviewByBookId(bookResult.payload.pKey);
			if (reviews.success)
			{
				var ratingResult = _bookService.SetRating(bookResult.payload, reviews.payload);
				if (!ratingResult.success) return ratingResult.msg;
			}

			return JsonConvert.SerializeObject(bookResult.payload);
		}
		[HttpGet("GetBookByTitle/{title}")]
		public string GetBookByTitle(string title)
		{
			var booksResult = _bookService.GetBookByTitle(title);
			if (!booksResult.success) return booksResult.msg;

			foreach (var book in booksResult.payload)
			{
				var reviews = _reviewService.GetReviewByBookId(book.pKey);
				if (reviews.success)
				{
					var ratingResult = _bookService.SetRating(book, reviews.payload);
					if(!ratingResult.success) return ratingResult.msg;
				}
			}

			return JsonConvert.SerializeObject(booksResult.payload);
		}
		[HttpPut("UpdateBook/{bookJson}")]
		public string UpdateBook(string bookJson)
		{
			var bookResult = JsonConvert.DeserializeObject<Book>(bookJson);
			var updateResult = _bookService.UpdateBook(bookResult.pKey, bookResult);

			return updateResult.success ? "success" : updateResult.msg;
		}
		[HttpDelete("DeleteBook/{bookId}")]
		public string DeleteBook(int bookId)
		{
			var deleteResult = _bookService.RemoveBooks([bookId]);
			return deleteResult.success ? "success" : deleteResult.msg;
		}
	}
}
