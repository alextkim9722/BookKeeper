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
			var book = JsonConvert.DeserializeObject<Book>(bookJson);
			var addResult = _bookService.AddBook(book);
			return JsonConvert.SerializeObject(addResult);
		}
		[HttpGet("GetBookById/{id}")]
		public string GetBookById(int id)
		{
			var bookResult = _bookService.GetBookById(id);

			if(bookResult.success)
			{
				var reviews = _reviewService.GetReviewByBookId(bookResult.payload.pKey);
				if (!reviews.success) return JsonConvert.SerializeObject(reviews);

				var ratingResult = _bookService.SetRating(bookResult.payload, reviews.payload);
				if (!ratingResult.success) return JsonConvert.SerializeObject(ratingResult);
			}

			return JsonConvert.SerializeObject(bookResult);
		}
		[HttpGet("GetBookByIsbn/{isbn}")]
		public string GetBookByIsbn(string isbn)
		{
			var bookResult = _bookService.GetBookByIsbn(isbn);

			if (bookResult.success)
			{
				var reviews = _reviewService.GetReviewByBookId(bookResult.payload.pKey);
				if (!reviews.success) return JsonConvert.SerializeObject(reviews);

				var ratingResult = _bookService.SetRating(bookResult.payload, reviews.payload);
				if (!ratingResult.success) return JsonConvert.SerializeObject(ratingResult);
			}

			return JsonConvert.SerializeObject(bookResult);
		}
		[HttpGet("GetBookByTitle/{title}")]
		public string GetBookByTitle(string title)
		{
			var booksResult = _bookService.GetBookByTitle(title);

			foreach (var book in booksResult.payload)
			{
				var reviews = _reviewService.GetReviewByBookId(book.pKey);
				if (!reviews.success) return JsonConvert.SerializeObject(reviews);

				var ratingResult = _bookService.SetRating(book, reviews.payload);
				if (!ratingResult.success) return JsonConvert.SerializeObject(ratingResult);
			}

			return JsonConvert.SerializeObject(booksResult);
		}
		[HttpGet("GetBooksByUser/{id}")]
		public string GetBooksByUser(int id)
		{
			var booksResult = _bookService.GetBooksByUser(id);

            foreach (var book in booksResult.payload)
            {
                var reviews = _reviewService.GetReviewByBookId(book.pKey);
                if (!reviews.success) return JsonConvert.SerializeObject(reviews);

                var ratingResult = _bookService.SetRating(book, reviews.payload);
                if (!ratingResult.success) return JsonConvert.SerializeObject(ratingResult);
            }

            return JsonConvert.SerializeObject(booksResult);
		}
		[HttpPut("UpdateBook/{bookJson}")]
		public string UpdateBook(string bookJson)
		{
			var book = JsonConvert.DeserializeObject<Book>(bookJson);
			var updateResult = _bookService.UpdateBook(book.pKey, book);

			return JsonConvert.SerializeObject(updateResult);
		}
		[HttpDelete("DeleteBook/{bookId}")]
		public string DeleteBook(int bookId)
		{
			var deleteResult = _bookService.RemoveBooks([bookId]);
			return JsonConvert.SerializeObject(deleteResult);
		}
	}
}
