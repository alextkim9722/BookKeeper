using BackEnd.Model;
using BackEnd.Services.ErrorHandling;
using BackEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEndTest.Services.TheoryDataGenerators;
using BackEndTest.Services.RandomGenerators;

namespace BackEndTest.Services
{
    [Collection("Test Integration With DB")]
	public class ReviewServiceTest : IClassFixture<TestDatabaseGenerator>
	{
		private readonly ReviewService _reviewService;

		// Test database services by comparing values between list data
		// and database/context data.
		public ReviewServiceTest(TestDatabaseGenerator generator)
		{
			var context = generator.createContext();
			_reviewService = new ReviewService(context);
		}

		[Fact]
		public void GetAllReviews_InvokedWithValidName_ReturnsAllReviewsWithNoNulls()
		{
			Results<IEnumerable<Review>> actual = _reviewService.getAllReviews();
			List<Review> expected = TestDatabaseGenerator.reviewTable
				.OrderBy(x => x.user_id).OrderBy(x => x.book_id).ToList();

			Assert.True(actual.success);

			for (int i = 0; i < expected.Count(); i++)
			{
				MappedComparator.compareReview(expected[i], actual.payload.ElementAt(i));
			}
		}

		[Fact]
		public void GetReviewByBookId_InvokedWithValidBookId_ReturnsReviewsWithNoNulls()
		{
			var id = TestDatabaseGenerator.reviewTable.ElementAt(0).book_id;
			Results<IEnumerable<Review>> actual = _reviewService.getReviewByBookId(id);
			List<Review> expected = TestDatabaseGenerator.reviewTable.Where(x => x.book_id == id)
				.OrderBy(x => x.user_id).OrderBy(x => x.book_id).ToList();

			Assert.True(actual.success);
			for(int i = 0;i < actual.payload.Count();i++)
			{
				MappedComparator.compareReview(expected[i], actual.payload.ElementAt(i));
			}
		}

		[Fact]
		public void GetReviewByUserId_InvokedWithValidUserId_ReturnsReviewsWithNoNulls()
		{
            var id = TestDatabaseGenerator.reviewTable.ElementAt(0).user_id;
            Results<IEnumerable<Review>> actual = _reviewService.getReviewByUserId(id);
			List<Review> expected = TestDatabaseGenerator.reviewTable.Where(x => x.user_id == id)
				.OrderBy(x => x.user_id).OrderBy(x => x.book_id).ToList();

			Assert.True(actual.success);
			for (int i = 0; i < actual.payload.Count(); i++)
			{
				MappedComparator.compareReview(expected[i], actual.payload.ElementAt(i));
			}
		}

		[Fact]
		public void AddReview_DeleteReview_InvokedWithValidReviewWithNoId_ReturnsSuccessResultAndExistsAndDeletedInDatabase()
		{
			var expected = _reviewService.removeReview(
				TestDatabaseGenerator.reviewTable[5].user_id,
				TestDatabaseGenerator.reviewTable[5].book_id);

			Results<Review> deletedActual = _reviewService.removeReview(expected.payload.user_id, expected.payload.book_id);
			Assert.False(deletedActual.success);

			Results<Review> add = _reviewService.addReview(expected.payload);
			Results<Review> actual = _reviewService.getReviewById(expected.payload.user_id, expected.payload.book_id);
			Assert.True(add.success);

			MappedComparator.compareReview(expected.payload, actual.payload);
		}

		[Fact]
		public void UpdateReview_InvokedWithProperIdAndUpdatedReviewWithSameId_ReturnsSuccessResult()
		{
			var expected = new Review()
			{
				user_id = TestDatabaseGenerator.reviewTable[1].user_id,
				book_id = TestDatabaseGenerator.reviewTable[1].book_id,
				description = "hello",
				date_submitted = new DateOnly(2002, 02, 02),
				rating = 1
			};

			var original = new Review()
			{
				user_id = TestDatabaseGenerator.reviewTable[1].user_id,
				book_id = TestDatabaseGenerator.reviewTable[1].book_id,
				description = TestDatabaseGenerator.reviewTable[1].description,
				date_submitted = TestDatabaseGenerator.reviewTable[1].date_submitted,
				rating = TestDatabaseGenerator.reviewTable[1].rating,
			};

			Results<Review> update = _reviewService.updateReview(expected.user_id, expected.book_id, expected);
			Results<Review> actual = _reviewService.getReviewById(expected.user_id, expected.book_id);

			Assert.True(update.success);
			MappedComparator.compareReview(expected, actual.payload);

			_reviewService.updateReview(original.user_id, original.book_id, original);
		}

		[Fact]
		public void RemoveReviewByUserId_InvokedWithNonExistantId_ReturnsFailedResult()
		{
			Results<Review> actual = _reviewService.getReviewById(50, 50);

			Assert.NotNull(actual);
			Assert.False(actual.success);
		}
	}
}
