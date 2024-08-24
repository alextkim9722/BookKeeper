using BackEnd.Model;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services.Interfaces
{
	public interface IReviewService
	{
		public Results<Review> AddReview(Review review);
		public Results<Review> GetReviewById(int userId, int bookId);
		public Results<IEnumerable<Review>> GetReviewByUserId(int id);
		public Results<IEnumerable<Review>> GetReviewByBookId(int id);
		public Results<Review> UpdateReview(int userId, int bookId, Review review);
		public Results<IEnumerable<Review>> RemoveReview(IEnumerable<int[]> id);
	}
}
