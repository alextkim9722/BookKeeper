using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Model;

namespace BackEndTest.Services.Comparator
{
    public class MappedComparator
    {
        public static void CompareBook(Book left, Book right)
        {
            Assert.NotNull(right);
            Assert.Equal(left.pKey, right.pKey);
            Assert.Equal(left.title, right.title);
            Assert.Equal(left.cover_picture, right.cover_picture);
            Assert.Equal(left.pages, right.pages);
            Assert.Equal(left.isbn, right.isbn);
        }

        public static void CompareUser(User left, User right)
        {
            Assert.NotNull(right);
            Assert.Equal(left.pKey, right.pKey);
            Assert.Equal(left.username, right.username);
            Assert.Equal(left.date_joined, right.date_joined);
            Assert.Equal(left.description, right.description);
            Assert.Equal(left.profile_picture, right.profile_picture);
        }

        public static void CompareGenre(Genre left, Genre right)
        {
            Assert.NotNull(right);
            Assert.Equal(left.pKey, right.pKey);
            Assert.Equal(left.genre_name, right.genre_name);
        }

        public static void CompareReview(Review left, Review right)
        {
            Assert.NotNull(right);
            Assert.Equal(left.firstKey, right.firstKey);
            Assert.Equal(left.secondKey, right.secondKey);
            Assert.Equal(left.description, right.description);
            Assert.Equal(left.date_submitted, right.date_submitted);
            Assert.Equal(left.rating, right.rating);
        }

        public static void CompareAuthor(Author left, Author right)
        {
            Assert.NotNull(right);
            Assert.Equal(left.pKey, right.pKey);
            Assert.Equal(left.first_name, right.first_name);
            Assert.Equal(left.middle_name, right.middle_name);
            Assert.Equal(left.last_name, right.last_name);
        }
    }
}
