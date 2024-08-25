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
using BackEndTest.Services.Comparator;

namespace BackEndTest.Services.ReviewServiceTests
{
	[Collection("Service Tests")]
	public class ReviewServiceUpdateTests : IClassFixture<ReviewDatabaseGenerator>
	{
		private readonly ReviewService _reviewService;
		private readonly BookShelfContext _bookShelfContext;

		public ReviewServiceUpdateTests(ReviewDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_reviewService = new ReviewService(
				new GenericService<Review>(_bookShelfContext));
		}

		[Fact]
		public void UpdateReview_Is3And1_ResultsSuccessful()
		{
			var userId = 3;
			var bookId = 1;
			var updatedModel = new Review()
			{
				firstKey = userId,
				secondKey = bookId,
				date_submitted = new DateOnly(2010, 10, 11),
				description = "october description!",
				rating = 7
			};

			var result = _reviewService.UpdateReview(userId, bookId, updatedModel);

			Assert.True(result.success);
			MappedComparator.CompareReview(_bookShelfContext.Review.Find(updatedModel.firstKey, updatedModel.secondKey), result.payload);
		}
		[Fact]
		public void UpdateReview_Is3And1NoDescription_ResultsSuccessful()
		{
			var userId = 3;
			var bookId = 1;
			var updatedModel = new Review()
			{
				firstKey = userId,
				secondKey = bookId,
				date_submitted = new DateOnly(2010, 10, 11),
				rating = 7
			};

			var result = _reviewService.UpdateReview(userId, bookId, updatedModel);

			Assert.True(result.success);
			MappedComparator.CompareReview(_bookShelfContext.Review.Find(updatedModel.firstKey, updatedModel.secondKey), result.payload);
		}
		[Fact]
		public void UpdateReview_Is3And1ModelNoDate_ResultsFailure()
		{
			var userId = 3;
			var bookId = 1;
			var errorMessage = "[System.String[]] Reviews need a date!";
			var updatedModel = new Review()
			{
				firstKey = userId,
				secondKey = bookId,
				description = "october description!",
				rating = 7
			};

			var result = _reviewService.UpdateReview(userId, bookId, updatedModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void UpdateReview_Is3And1ModelRating15_ResultsFailure()
		{
			var userId = 3;
			var bookId = 1;
			var errorMessage = "[System.String[]] Rating must be between 0 and 10!";
			var updatedModel = new Review()
			{
				firstKey = userId,
				secondKey = bookId,
				description = "october description!",
				date_submitted = new DateOnly(2010, 10, 11),
				rating = 15
			};

			var result = _reviewService.UpdateReview(userId, bookId, updatedModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
		[Fact]
		public void UpdateReview_Is3And1ModelRatingNegative1_ResultsFailure()
		{
			var userId = 3;
			var bookId = 1;
			var errorMessage = "[System.String[]] Rating must be between 0 and 10!";
			var updatedModel = new Review()
			{
				firstKey = userId,
				secondKey = bookId,
				description = "october description!",
				date_submitted = new DateOnly(2010, 10, 11),
				rating = -1
			};

			var result = _reviewService.UpdateReview(userId, bookId, updatedModel);

			Assert.False(result.success);
			ErrorComparator.CompareErrors(errorMessage, result.msg);
		}
	}
}
