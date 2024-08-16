using BackEnd.Services.Abstracts;
using BackEnd.Services.Interfaces;
using BackEnd.Model;

namespace BackEnd.Services
{
	public class ReviewService : TableServiceAbstract<Review>, IReviewService
	{
		private readonly Callback handler1;

		public ReviewService(BookShelfContext bookShelfContext) :
			base(bookShelfContext)
		{
		}

		public Review? addReview(Review review)
			=> addReview(review);

		public Review? removeReview(int userId, int bookId)
			=> deleteModel(
				x => x.user_id == userId && x.book_id == bookId);

		public Review? updateReview(
			int userId, 
			int bookId, 
			Review review)
		{
			Review? updatedReview = updateModel(
				x => x.user_id == userId && x.book_id == bookId,
				review);


			return updatedReview;
		}

		public Review? getReviewByBookId(int id)
			=> formatModel(null, x => x.book_id == id);

		public Review? getReviewByUserId(int id)
			=> formatModel(null, x => x.user_id == id);

		public IEnumerable<Review>? getAllReviews()
			=> getAllModels();
	}
}
