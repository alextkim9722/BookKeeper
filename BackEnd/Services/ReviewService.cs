using BackEnd.Services.Abstracts;
using BackEnd.Services.Interfaces;
using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services
{
	public class ReviewService : TableServiceAbstract<Review>, IReviewService
	{
		private const int MAX_RATING = 10;
		private const int MIN_RATING = 0;

		public ReviewService(BookShelfContext bookShelfContext) :
			base(bookShelfContext)
		{
			// Empty
		}

		public Results<Review> addReview(Review review)
			=> addReview(review);

		public Results<Review> removeReview(int userId, int bookId)
			=> deleteModel(
				x => x.user_id == userId && x.book_id == bookId);

		// Using model mapped so it doesn't turn review null when updating.
		public Results<Review> updateReview(
			int userId, 
			int bookId, 
			Review review)
			=> updateModelMapped(
				x => x.user_id == userId && x.book_id == bookId,
				review);

		public Results<Review> getReviewByBookId(int id)
			=> formatModel(x => x.book_id == id);

		public Results<Review> getReviewByUserId(int id)
			=> formatModel(x => x.user_id == id);

		public Results<IEnumerable<Review>> getAllReviews()
			=> getAllModels();

		protected override Review transferProperties(Review original, Review updated)
		{
			throw new NotImplementedException();
		}

		protected override Results<Review> validateProperties(Review review)
			=> new ResultsIntegerBetweenInclusive<Review>(
				review, MIN_RATING, MAX_RATING, review.rating);
	}
}
