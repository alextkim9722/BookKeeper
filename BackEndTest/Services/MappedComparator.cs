using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Model;

namespace BackEndTest.Services
{
	public class MappedComparator
	{
		public static void compareBook(Book left, Book right)
		{
			Assert.NotNull(right);
			Assert.Equal(left.book_id, right.book_id);
			Assert.Equal(left.title, right.title);
			Assert.Equal(left.cover_picture, right.cover_picture);
			Assert.Equal(left.pages, right.pages);
			Assert.Equal(left.isbn, right.isbn);
			Assert.Equal(left.rating, right.rating);
		}

		public static void compareUser(User left, User right)
		{
			Assert.NotNull(right);
			Assert.Equal(left.user_id, right.user_id);
			Assert.Equal(left.username, right.username);
			Assert.Equal(left.date_joined, right.date_joined);
			Assert.Equal(left.description, right.description);
			Assert.Equal(left.profile_picture, right.profile_picture);
		}

		public static void compareGenre(Genre left, Genre right)
		{
			Assert.NotNull(right);
			Assert.Equal(left.genre_id, right.genre_id);
			Assert.Equal(left.genre_name, right.genre_name);
		}

		public static void compareReview(Review left, Review right)
		{
			Assert.NotNull(right);
			Assert.Equal(left.book_id, right.book_id);
			Assert.Equal(left.user_id, right.user_id);
			Assert.Equal(left.description, right.description);
			Assert.Equal(left.date_submitted, right.date_submitted);
			Assert.Equal(left.rating, right.rating);
		}

		public static void compareAuthor(Author left, Author right)
		{
			Assert.NotNull(right);
			Assert.Equal(left.author_id, right.author_id);
			Assert.Equal(left.first_name, right.first_name);
			Assert.Equal(left.middle_name, right.middle_name);
			Assert.Equal(left.last_name, right.last_name);
		}
	}
}
