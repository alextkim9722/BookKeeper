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

namespace BackEndTest.Services.ReviewServiceTests
{
	[Collection("Service Tests")]
	public class ReviewServiceDeleteTest : IClassFixture<ReviewDatabaseGenerator>
	{
		private readonly ReviewService _reviewService;
		private readonly BookShelfContext _bookShelfContext;

		public ReviewServiceDeleteTest(ReviewDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_reviewService = new ReviewService(
				new GenericService<Review>(_bookShelfContext));
		}

		[Fact]
		public void RemoveReviews_IsId3And1_ResultsSuccessfulFailureToFind()
		{
			var userId = 3;
			var bookId = 1;

			var result = _reviewService.RemoveReview([[userId, bookId]]);

			Assert.True(result.success);
			Assert.Null(_bookShelfContext.Review.Find(userId, bookId));
		}
	}
}
