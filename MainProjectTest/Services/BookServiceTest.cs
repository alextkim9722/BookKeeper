using MainProject.Datastore;
using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProjectTest.Services
{
	public class BookServiceTest
	{
		private readonly Mock<IBookRepository> _bookRepository;
		private readonly Mock<BookService> _bookService;

		public BookServiceTest()
		{
			_bookRepository = new Mock<IBookRepository>();
			_bookService = new Mock<BookService>(_bookRepository.Object);

			_bookRepository.Setup(s => s.getBookByID(1)).Returns(new BookModel {
				book_id = 1,
				title = "red",
				pages = 2000,
				isbn = "redisbn",
				rating = 2,
				cover_picture = "redpic"
			});
		}

		[Fact]
		public void TEST_BOOK_FORMAT_OPTION_WITH_OUT_CREATION()
		{
			List<AuthorModel> expectedAuthor2 = new List<AuthorModel>() {
				new AuthorModel()
				{
					author_id = 1,
					first_name = "blare",
					middle_name = "witch",
					last_name = "craft",
					full_name = "blare witch craft"
				},
				new AuthorModel()
				{
					author_id = 2,
					first_name = "mark",
					middle_name = "a",
					last_name = "blue",
					full_name = "mark a blue"
				}
			};

			List<GenreModel> expectedGenre1 = new List<GenreModel>() {
				new GenreModel()
				{
					genre_id = 1,
					genre_name = "SCIFI"
				},
				new GenreModel()
				{
					genre_id = 2,
					genre_name = "FANTASY"
				}
			};

			BookModel target = _bookService.Object.createBookModel(1, expectedAuthor2, expectedGenre1);
			Assert.NotNull(target.authors);
			Assert.NotNull(target.genres);
			Assert.Equal("mark",target.authors.ElementAt(1).first_name);
		}

		[Fact]
		public void TEST_BOOK_FORMAT_OPTION_WITH_CREATION()
		{
			List<AuthorModel> expectedAuthor2 = new List<AuthorModel>() {
				new AuthorModel()
				{
					author_id = 1,
					first_name = "blare",
					middle_name = "witch",
					last_name = "craft",
					full_name = "blare witch craft"
				},
				new AuthorModel()
				{
					author_id = 2,
					first_name = "mark",
					middle_name = "a",
					last_name = "blue",
					full_name = "mark a blue"
				}
			};

			List<GenreModel> expectedGenre1 = new List<GenreModel>() {
				new GenreModel()
				{
					genre_id = 1,
					genre_name = "SCIFI"
				},
				new GenreModel()
				{
					genre_id = 2,
					genre_name = "FANTASY"
				}
			};

			BookModel target = new BookModel
			{
				book_id = 1,
				title = "red",
				pages = 2000,
				isbn = "redisbn",
				rating = 2,
				cover_picture = "redpic"
			};
			_bookService.Object.formatBookModel(target, expectedAuthor2, expectedGenre1);
			Assert.NotNull(target.authors);
			Assert.NotNull(target.genres);
			Assert.Equal("mark", target.authors.ElementAt(1).first_name);
		}
	}
}
