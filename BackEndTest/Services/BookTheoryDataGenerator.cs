using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Model;
using Microsoft.IdentityModel.Tokens;

namespace BackEndTest.Services
{
	public class BookTheoryDataGenerator : TheoryData<Book>
	{
		public BookTheoryDataGenerator() 
		{
			for (int i = 0; i < 20; i++)
			{
				Book book = TestDatabaseGenerator.bookTable[i];
				book.authors = TestDatabaseGenerator.authorTable.Where(
						x => TestDatabaseGenerator.bookAuthorTable
						.Where(y => y.book_id == i + 1)
						.Select(y => y.author_id)
						.ToList()
						.Contains(x.author_id))
						.ToList();
				book.genres = TestDatabaseGenerator.genreTable.Where(
						x => TestDatabaseGenerator.bookGenreTable
						.Where(y => y.book_id == i + 1)
						.Select(y => y.genre_id)
						.ToList()
						.Contains(x.genre_id))
						.ToList();
				book.readers = TestDatabaseGenerator.userTable.Where(
						x => TestDatabaseGenerator.userBookTable
						.Where(y => y.book_id == i + 1)
						.Select(y => y.user_id)
						.ToList()
						.Contains(x.user_id))
						.ToList().Count();
				book.reviews = TestDatabaseGenerator.reviewTable
						.Where(y => y.book_id == i + 1)
						.OrderBy(x => x.user_id).ToList();

				if (!book.reviews.IsNullOrEmpty())
				{
					book.rating = Convert.ToInt32(book.reviews!.Select(x => x.rating).Average());
				}
				else
				{
					book.rating = 0;
				}

				Add(book);
			}
		}
	}
}
