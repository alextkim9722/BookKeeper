using BackEnd.ErrorHandling;
using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
    public interface IReviewService
	{
		public Results<Review> addReview(Review review);
		public Results<Review> removeReview(int userId, int bookId);
		public Results<Review> updateReview(int userId, int bookId, Review review);
		public Results<Review> getReviewById(int userId, int bookId);
		public Results<IEnumerable<Review>> getReviewByBookId(int id);
		public Results<IEnumerable<Review>> getReviewByUserId(int id);
		public Results<IEnumerable<Review>> getAllReviews();
	}
}
