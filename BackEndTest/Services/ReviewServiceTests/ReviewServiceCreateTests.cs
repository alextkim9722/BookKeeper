using BackEnd.Model;
using BackEnd.Services.Context;
using BackEnd.Services.Generics;
using BackEnd.Services;
using BackEndTest.Services.DatabaseGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Services.Interfaces;
using BackEndTest.Services.Comparator;

namespace BackEndTest.Services.ReviewServiceTests
{
	[Collection("Service Tests")]
	public class ReviewServiceCreateTests : IClassFixture<ReviewDatabaseGenerator>
	{
		private readonly ReviewService _reviewService;
		private readonly BookShelfContext _bookShelfContext;

		public ReviewServiceCreateTests(ReviewDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_reviewService = new ReviewService(
				new GenericService<Review>(_bookShelfContext));
		}

		[Fact]
		public void AddReview_IsReviewModel_ResultsSuccessful()
		{
			var reviewModel = new Review()
			{
				firstKey = 1,
				secondKey = 2,
				date_submitted = new DateOnly(2010, 10, 11),
				description = "october description!",
				rating = 7
			};

			var result = _reviewService.AddReview(reviewModel);

			Assert.True(result.success);
			MappedComparator.CompareReview(_bookShelfContext.Review.Find(reviewModel.firstKey, reviewModel.secondKey), result.payload);
		}
		[Fact]
		public void AddReview_IsReviewModelNoDescription_ResultsSuccessful()
		{
			var reviewModel = new Review()
			{
				firstKey = 3,
				secondKey = 2,
				date_submitted = new DateOnly(2010, 10, 11),
				rating = 7
			};

			var result = _reviewService.AddReview(reviewModel);

			Assert.True(result.success);
			MappedComparator.CompareReview(_bookShelfContext.Review.Find(reviewModel.firstKey, reviewModel.secondKey), result.payload);
		}
		[Fact]
		public void AddReview_IsReviewModelNoDate_ResultsFailure()
		{
			var errorMessage = "[System.String[]] Reviews need a date!";
			var reviewModel = new Review()
			{
				firstKey = 1,
				secondKey = 3,
				description = "october description!",
				rating = 7
			};

			var result = _reviewService.AddReview(reviewModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddReview_IsReviewModelRating15_ResultsFailure()
		{
			var errorMessage = "[System.String[]] Rating must be between 0 and 10!";
			var reviewModel = new Review()
			{
				firstKey = 3,
				secondKey = 3,
				description = "october description!",
				date_submitted = new DateOnly(2010, 10, 11),
				rating = 15
			};

			var result = _reviewService.AddReview(reviewModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void AddReview_IsReviewModelRatingNegative1_ResultsFailure()
		{
			var errorMessage = "[System.String[]] Rating must be between 0 and 10!";
			var reviewModel = new Review()
			{
				firstKey = 3,
				secondKey = 3,
				description = "october description!",
				date_submitted = new DateOnly(2010, 10, 11),
				rating = -1
			};

			var result = _reviewService.AddReview(reviewModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
	}
}
