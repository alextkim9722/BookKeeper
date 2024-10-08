﻿using BackEnd.Services.Interfaces;
using BackEnd.Model;
using BackEnd.Services.ErrorHandling;
using BackEnd.Services.Generics.Interfaces;

namespace BackEnd.Services
{
	public class ReviewService : IReviewService
	{
		private readonly IGenericService<Review> _genericService;
		public ReviewService(IGenericService<Review> genericService)
		{
			_genericService = genericService;
		}

		public Results<Review> AddReview(Review review)
			=> _genericService.AddModel(review);
		public Results<Review> GetReviewById(int userId, int bookId)
			=> _genericService.ProcessUniqueModel(x => x.firstKey == userId && x.secondKey == bookId);
		public Results<IEnumerable<Review>> GetReviewByUserId(int id)
			=> SortReviews(_genericService.ProcessModels(x => x.firstKey == id));
		public Results<IEnumerable<Review>> GetReviewByBookId(int id)
			=> SortReviews(_genericService.ProcessModels(x => x.secondKey == id));
		public Results<Review> UpdateReview(int userId, int bookId, Review review)
			=> _genericService.UpdateModel(review, userId, bookId);
		public Results<IEnumerable<Review>> RemoveReview(IEnumerable<int[]> id)
			=> _genericService.DeleteModels(id);

		private Results<IEnumerable<Review>> SortReviews(Results<IEnumerable<Review>> reviews)
		{
            reviews.payload = reviews.payload.OrderByDescending(x => x.date_submitted);
			return reviews;
		}
	}
}
