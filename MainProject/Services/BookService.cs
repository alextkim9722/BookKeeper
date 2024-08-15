using MainProject.Datastore;
using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using Xunit.Sdk;

namespace MainProject.Services
{
	public class BookService : JoinServiceAbstract<Book>, IBookService
	{
		public BookService(BookShelfContext bookShelfContext) :
			base(bookShelfContext, "book_id", "book") { }

		public Book? addBook(Book book)
			=> addModel(book);

		public Book? removeBook(int id)
		{
			if (deleteJoins<Book_Author>(x => x.book_id == id) &&
				deleteJoins<Book_Genre>(x => x.book_id == id) &&
				deleteJoins<User_Book>(x => x.book_id == id))
			{
				return deleteModel(x => x.book_id == id);
			}
			else
			{
				return null;
			}
		}

		public Book? updateBook(int id, Book book)
		{
			Book? updatedBook = updateModel(id, book);

			if (updatedBook != null)
			{
				updatedBook.authors = book.authors;
				updatedBook.genres = book.genres;
				updatedBook.readers = book.readers;

				return updatedBook;
			}
			else
			{
				return null;
			}
		}

		public Book? getBookById(int id)
			=> formatBook(x => x.book_id == id);

		public Book? getBookByIsbn(string isbn)
			=> formatBook(x => x.isbn == isbn);

		public Book? getBookByTitle(string title)
			=> formatBook(x => x.title == title);

		private Book? formatBook(Expression<Func<Book, bool>> condition)
		{
			Book? model = getModelBy(condition);

			if (model != null)
			{
				model = addBridges(model);
			}

			return model;
		}

		private Book addBridges(Book book)
		{
			book.authors = getMultipleJoins<Author, Book_Author>(x => x.book_id == book.book_id, y => y.author_id);
			book.genres = getMultipleJoins<Genre, Book_Genre>(x => x.book_id == book.book_id, y => y.genre_id);

			var readersModels = getMultipleJoins<User, User_Book>(x => x.book_id == book.book_id, y => y.user_id);

			if(readersModels != null)
			{
				book.readers = readersModels.Count();
			}else
			{
				book.readers = null;
			}

			return book;
		}

		public IEnumerable<Book>? getAllBooks()
		{
			var books = getAllModels();
			if (books != null)
			{
				books.Select(
					x => {
						x = formatBook(y => y.book_id == x.book_id);
						return x; 
					}).ToList();

				return books;
			}
			else
			{
				return null;
			}
        }
	}
}
