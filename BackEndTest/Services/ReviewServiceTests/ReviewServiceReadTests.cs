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
	public class ReviewServiceReadTests : IClassFixture<ReviewDatabaseGenerator>
	{
		private readonly ReviewService _reviewService;
		private readonly BookShelfContext _bookShelfContext;

		public ReviewServiceReadTests(ReviewDatabaseGenerator generator)
		{
			_bookShelfContext = generator.GetContext();
			_reviewService = new ReviewService(
				new GenericService<Review>(_bookShelfContext));
		}

		[Fact]
		public void GetReviewById_Is3And1_ResultsSuccessfulAndSuccessfulFind()
		{
			var userId = 3;
			var bookId = 1;
			var expected = new Review()
			{
				firstKey = 3,
				secondKey = 1,
				date_submitted = new DateOnly(2020, 02, 22),
				description = "red description!",
				rating = 4
			};

			var result = _reviewService.GetReviewById(userId, bookId);

			Assert.True(result.success);
			MappedComparator.CompareReview(expected, result.payload);
		}
		[Fact]
		public void GetReviewByUserId_Is1_ResultsSuccessfulAndSuccessfulFind()
		{
			var userId = 1;
			var expected = _bookShelfContext.Review.Where(x => x.firstKey == userId).OrderBy(x => x.firstKey).ToList();

			var result = _reviewService.GetReviewByUserId(userId);
			var resultList = result.payload.OrderBy(x => x.firstKey).ToList();

			Assert.True(result.success);
			for (int i = 0; i < expected.Count; i++)
			{
				MappedComparator.CompareReview(expected[i], resultList[i]);
			}
		}
		[Fact]
		public void GetReviewByBookId_Is1_ResultsSuccessfulAndSuccessfulFind()
		{
			var bookId = 1;
			var expected = _bookShelfContext.Review.Where(x => x.secondKey == bookId).OrderBy(x => x.secondKey).ToList();

			var result = _reviewService.GetReviewByBookId(bookId);
			var resultList = result.payload.OrderBy(x => x.secondKey).ToList();

			Assert.True(result.success);
			for (int i = 0; i < expected.Count; i++)
			{
				MappedComparator.CompareReview(expected[i], resultList[i]);
			}
		}
	}
}
