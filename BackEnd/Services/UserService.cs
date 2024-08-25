﻿using BackEnd.Model;
using BackEnd.Services.Interfaces;
using BackEnd.Services.Generics.Interfaces;
using Microsoft.Data.SqlClient;
using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services
{
	public class UserService : IUserService
	{
		private readonly IGenericService<User> _genericService;
		private readonly IJunctionService<User_Book> _jUserBookService;
		private readonly IJunctionService<Review> _jReviewService;

		public UserService(
			IGenericService<User> genericService,
			IJunctionService<User_Book> jUserBookService,
			IJunctionService<Review> jReviewService)
		{
			_genericService = genericService;
			_jUserBookService = jUserBookService;
			_jReviewService = jReviewService;
		}

		public Results<User> AddUser(User user)
			=> _genericService.AddModel(user);
		public Results<User> GetUserById(int id)
			=> _genericService.ProcessUniqueModel(x => x.pKey == id, AddDependents);
		public Results<User> GetUserByIdentificationId(string id)
			=> _genericService.ProcessUniqueModel(x => x.identification_id == id, AddDependents);
		public Results<IEnumerable<User>> GetUserByUserName(string username)
			=> _genericService.ProcessModels(x => x.username == username, AddDependents);
		public Results<User> UpdateUser(int id, User user)
			=> _genericService.UpdateModel(user, id);
		public Results<IEnumerable<User>> RemoveUser(IEnumerable<int> id)
			=> _genericService.DeleteModels([id.ToArray()], DeleteDependents);

		private Results<User> AddingProcess(User user)
		{
			var dependentsResult = AddDependents(user);

			return dependentsResult;
		}

		public Results<IEnumerable<Book>> SetBooksAndPagesRead(User user, IEnumerable<Book> books)
		{
			var bookIds = books.Select(x => x.pKey).ToList().OrderBy(x => x);
			if(user.books.OrderBy(x => x) == bookIds)
			{
				user.booksRead = books.Count();
				user.pagesRead = books.Sum(x => x.pages);

				return new ResultsSuccessful<IEnumerable<Book>>(books);
			}
			else
			{
				return new ResultsFailure<IEnumerable<Book>>("Not all books are read by the user!");
			}
		}
		private Results<User> AddDependents(User user)
		{
			try
			{
				user.books = _jUserBookService.GetJunctionedJoinedModelsId(user.pKey, true);
				user.reviews = _jReviewService.GetJunctionedJoinedModelsId(user.pKey, true);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<User>("Failed to add bridges");
			}

			return new ResultsSuccessful<User>(user);
		}
		private Results<User> DeleteDependents(User User)
		{
			try
			{
				_jUserBookService.DeleteJunctionModels(User.pKey, true);
				_jReviewService.DeleteJunctionModels(User.pKey, true);
			}
			catch (SqlException ex)
			{
				return new ResultsFailure<User>("Failed to add bridges");
			}

			return new ResultsSuccessful<User>(User);
		}
	}
}
