using BackEnd.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
	public class AuthorTheoryDataGenerator : TheoryData<Author>
	{
		public AuthorTheoryDataGenerator()
		{
			for (int i = 0; i < 20; i++)
			{
				Author author = TestDatabaseGenerator.authorTable[i];
				author.books = TestDatabaseGenerator.bookTable.Where(
						x => TestDatabaseGenerator.bookAuthorTable
						.Where(y => y.author_id == i + 1)
						.Select(y => y.book_id)
						.ToList()
						.Contains(x.book_id))
						.ToList();
				if(author.middle_name.IsNullOrEmpty())
				{
					author.full_name = author.first_name + " " + author.last_name;
				}
				else
				{
					author.full_name = author.first_name + " " + author.middle_name + " " + author.last_name;
				}

				Add(author);
			}
		}
	}
}
