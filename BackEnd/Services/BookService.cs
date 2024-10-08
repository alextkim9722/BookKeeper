﻿using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using BackEnd.Services.Generics.Interfaces;
using Microsoft.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services
{
	public class BookService : IBookService
	{
		private readonly IGenericService<Book> _genericService;
		private readonly IJunctionService<Book_Author> _jBookAuthorService;
		private readonly IJunctionService<Book_Genre> _jBookGenreService;
		private readonly IJunctionService<User_Book> _jUserBookService;
		private readonly IJunctionService<Review> _jReviewService;

		public BookService(
			IGenericService<Book> genericService,
			IJunctionService<Book_Author> jBookAuthorService,
			IJunctionService<Book_Genre> jBookGenreService,
			IJunctionService<User_Book> jUserBookService,
			IJunctionService<Review> jReviewService)
		{
			_genericService = genericService;
			_jBookAuthorService = jBookAuthorService;
			_jBookGenreService = jBookGenreService;
			_jUserBookService = jUserBookService;
			_jReviewService = jReviewService;
		}

		public Results<Book> AddBook(Book book)
			=> _genericService.AddModel(book);
		public Results<Book> GetBookById(int id)
			=> _genericService.ProcessUniqueModel(x => x.pKey == id, AddDependents);
		public Results<Book> GetBookByIsbn(string isbn)
			=> _genericService.ProcessUniqueModel(x => x.isbn == isbn, AddDependents);
		public Results<IEnumerable<Book>> GetBookByTitle(string title)
			=> _genericService.ProcessModels(x => x.title == title, AddDependents);
		public Results<IEnumerable<Book>> GetBooksByUser(int userId)
		{
			var bookIds  = _jUserBookService.GetJunctionedJoinedModelsId(userId, true).ToList();
			var books = new List<Book>();
			foreach(var bookId in bookIds)
			{
				var result = _genericService.ProcessUniqueModel(x => x.pKey == bookId, AddDependents);
				if (result.success) books.Add(result.payload);
			}

			return new ResultsSuccessful<IEnumerable<Book>>(books);
		}

		public Results<Book> UpdateBook(int id, Book book)
			=> _genericService.UpdateModel(book, id);
		public Results<IEnumerable<Book>> RemoveBooks(IEnumerable<int> id)
			=> _genericService.DeleteModels([id.ToArray()], DeleteDependents);

		public Results<IEnumerable<Review>> SetRating(Book book, IEnumerable<Review> review)
		{
			var userIds = review.OrderBy(x => x.firstKey).ToList();
			var bookUserIds = book.reviews.OrderBy(x => x).ToList();

			if(userIds.Count() == book.reviews.Count())
			{
				for (var i = 0; i < userIds.Count(); i++)
				{
					if (!(userIds.ElementAt(i).firstKey == bookUserIds.ElementAt(i) &&
						userIds.ElementAt(i).secondKey == book.pKey))
					{
						return new ResultsFailure<IEnumerable<Review>>("Not all reviews are from readers who read the book!");
					}
				}

				book.rating = Convert.ToInt32(review.Average(x => x.rating));

				return new ResultsSuccessful<IEnumerable<Review>>(review);
			}
			else
			{
				return new ResultsFailure<IEnumerable<Review>>("Too many reviews!");
			}
		}

		private Results<Book> AddDependents(Book book)
		{
			try
			{
				book.authors = _jBookAuthorService.GetJunctionedJoinedModelsId(book.pKey, true);
				book.genres = _jBookGenreService.GetJunctionedJoinedModelsId(book.pKey, true);
				book.users = _jUserBookService.GetJunctionedJoinedModelsId(book.pKey, false);
				book.reviews = _jReviewService.GetJunctionedJoinedModelsId(book.pKey, false);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<Book>("Failed to add bridges");
			}

			return new ResultsSuccessful<Book>(book);
		}
		private Results<Book> DeleteDependents(Book book)
		{
			try
			{
				_jBookAuthorService.DeleteJunctionModels(book.pKey, true);
				_jBookGenreService.DeleteJunctionModels(book.pKey, true);
				_jUserBookService.DeleteJunctionModels(book.pKey, false);
				_jReviewService.DeleteJunctionModels(book.pKey, false);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<Book>("Failed to add bridges");
			}

			return new ResultsSuccessful<Book>(book);
		}
	}
}
