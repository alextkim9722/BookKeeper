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
		public static TestDatabaseGenerator generator = new TestDatabaseGenerator();

		public BookTheoryDataGenerator() 
		{
			for (int i = 0; i < 20; i++)
			{
				Book book = generator.bookTable[i];
				book.authors = generator.authorTable.Where(
						x => generator.bookAuthorTable
						.Where(y => y.book_id == i + 1)
						.Select(y => y.author_id)
						.ToList()
						.Contains(x.author_id))
						.ToList();
				book.genres = generator.genreTable.Where(
						x => generator.bookGenreTable
						.Where(y => y.book_id == i + 1)
						.Select(y => y.genre_id)
						.ToList()
						.Contains(x.genre_id))
						.ToList();
				book.readers = generator.userTable.Where(
						x => generator.userBookTable
						.Where(y => y.book_id == i + 1)
						.Select(y => y.user_id)
						.ToList()
						.Contains(x.user_id))
						.ToList().Count();
				book.reviews = generator.reviewTable
						.Where(y => y.book_id == i + 1)
						.ToList();

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
