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

		[HttpPost("AddReview/{reviewJson}")]
		public string AddReview(string reviewJson)
		{
			var reviewResult = JsonConvert.DeserializeObject<Review>(reviewJson);
			var addResult = _reviewService.AddReview(reviewResult);

			return addResult.success ? "success" : addResult.msg;
		}
		[HttpGet("GetReviewById/{userId}/{bookId}")]
		public string GetReviewById(int userId, int bookId)
		{
			var reviewResult = _reviewService.GetReviewById(userId, bookId);
			if (!reviewResult.success) return reviewResult.msg;

			return JsonConvert.SerializeObject(reviewResult.payload);
		}
		[HttpGet("GetReviewByUserId/{userId}")]
		public string GetReviewByUserId(int userId)
		{
			var reviewsResult = _reviewService.GetReviewByUserId(userId);
			if (!reviewsResult.success) return reviewsResult.msg;

			return JsonConvert.SerializeObject(reviewsResult.payload);
		}
		[HttpGet("GetReviewByBookId/{bookId}")]
		public string GetReviewByBookId(int bookId)
		{
			var reviewsResult = _reviewService.GetReviewByBookId(bookId);
			if (!reviewsResult.success) return reviewsResult.msg;

			return JsonConvert.SerializeObject(reviewsResult.payload);
		}
		[HttpPut("UpdateReview/{reviewJson}")]
		public string UpdateReview(string reviewJson)
		{
			var reviewResult = JsonConvert.DeserializeObject<Review>(reviewJson);
			var addResult = _reviewService.UpdateReview(reviewResult.firstKey, reviewResult.secondKey, reviewResult);

			return addResult.success ? "success" : addResult.msg;
		}
		[HttpDelete("DeleteReview/{userId}/{bookId}")]
		public string DeleteReview(int userId, int bookId)
		{
			var deleteResult = _reviewService.RemoveReview([[userId, bookId]]);
			return deleteResult.success ? "success" : deleteResult.msg;
		}
	}
}
