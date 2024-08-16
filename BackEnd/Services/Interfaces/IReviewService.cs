using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
	public interface IReviewService
	{
		public Review? addReview(Review review);
		public Review? removeReview(int userId, int bookId);
		public Review? updateReview(int userId, int bookId, Review review);
		public Review? getReviewByBookId(int id);
		public Review? getReviewByUserId(int id);
		public IEnumerable<Review> getAllReviews();
	}
}
