using BackEnd.Model;
using BackEnd.Services;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		private readonly IReviewService _reviewService;

		public ReviewController(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		[HttpPost("AddReview")]
		public string AddReview([FromBody]Review review)
		{
			var addResult = _reviewService.AddReview(review);
			return JsonConvert.SerializeObject(addResult);
		}
		[HttpGet("GetReviewById/{userId}/{bookId}")]
		public string GetReviewById(int userId, int bookId)
		{
			var reviewResult = _reviewService.GetReviewById(userId, bookId);
			return JsonConvert.SerializeObject(reviewResult);
		}
		[HttpGet("GetReviewByUserId/{userId}")]
		public string GetReviewByUserId(int userId)
		{
			var reviewsResult = _reviewService.GetReviewByUserId(userId);
			return JsonConvert.SerializeObject(reviewsResult);
		}
		[HttpGet("GetReviewByBookId/{bookId}")]
		public string GetReviewByBookId(int bookId)
		{
			var reviewsResult = _reviewService.GetReviewByBookId(bookId);
			return JsonConvert.SerializeObject(reviewsResult);
		}
		[HttpPut("UpdateReview/{reviewJson}")]
		public string UpdateReview(string reviewJson)
		{
			var review = JsonConvert.DeserializeObject<Review>(reviewJson);
			var updateResult = _reviewService.UpdateReview(review.firstKey, review.secondKey, review);
			return JsonConvert.SerializeObject(updateResult);
		}
		[HttpDelete("DeleteReview/{userId}/{bookId}")]
		public string DeleteReview(int userId, int bookId)
		{
			var deleteResult = _reviewService.RemoveReview([[userId, bookId]]);
			return JsonConvert.SerializeObject(deleteResult);
		}
	}
}
