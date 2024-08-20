using BackEnd.Model;
using BackEndTest.Services.RandomGenerators;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.TheoryDataGenerators
{
    public class UserTheoryDataGenerator : TheoryData<User>
    {
        public UserTheoryDataGenerator()
        {
            for (int i = 0; i < TestDatabaseGenerator._tableValueCount; i++)
            {
                User user = TestDatabaseGenerator.userTable[i];
                user.books = TestDatabaseGenerator.bookTable.Where(
                        x => TestDatabaseGenerator.userBookTable
                        .Where(y => y.user_id == i + 1)
                        .Select(y => y.book_id)
                        .ToList()
                        .Contains(x.book_id))
                        .ToList();

                user.reviews = TestDatabaseGenerator.reviewTable
                        .Where(y => y.user_id == i + 1)
                        .OrderBy(x => x.book_id).ToList();

                if (!user.books.IsNullOrEmpty())
                {
                    user.pagesRead = user.books!.Select(x => x.pages).Sum();
                    user.booksRead = user.books!.Count();
                }

                Add(user);
            }
        }
    }
}
