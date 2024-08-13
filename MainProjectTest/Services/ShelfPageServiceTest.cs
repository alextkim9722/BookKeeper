using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.Services;
using MainProject.Services.Interfaces;
using MainProject.ViewModel;
using Moq;

namespace MainProjectTest.Services
{
    public class ShelfPageServiceTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IAuthorService> _authorService;
        private readonly Mock<IBookService> _bookService;
        private readonly Mock<IGenreService> _genreService;
        private readonly Mock<ShelfPageService> _shelfPageService;

        public static TheoryData<int, string, DateOnly, string, string> Cases =
            new()
            {
                {1, "bobby14", new DateOnly(2013,11,16), "hello im bobby", "no pic"},
                {2, "redman182", new DateOnly(2010, 2, 4), "hello im red", "no pic"},
                {3, "christopher77", new DateOnly(2001, 07, 25), "hello im chris", "no pic"}
            };

        public ShelfPageServiceTest()
        {
			_userRepository = new Mock<IUserRepository>();
			_authorService = new Mock<IAuthorService>();
			_bookService = new Mock<IBookService>();
			_genreService = new Mock<IGenreService>();
			_shelfPageService = new Mock<ShelfPageService>(
				_userRepository.Object,
				_authorService.Object,
				_genreService.Object,
				_bookService.Object);
		}

        [Theory, MemberData(nameof(Cases))]
        public void TEST_MODEL_CREATION_FOR_SHELFPAGEVIEWMODEL(int id, string exName, DateOnly exDate, string exDesc, string exPic)
        {
			_userRepository.Setup(s => s.getUserById(1)).Returns(new UserModel()
			{
				user_id = 1,
				username = "bobby14",
				date_joined = new DateOnly(2013, 11, 16),
				description = "hello im bobby",
				profile_picture = "no pic"
			});
			_userRepository.Setup(s => s.getUserById(2)).Returns(new UserModel()
			{
				user_id = 2,
				username = "redman182",
				date_joined = new DateOnly(2010, 2, 4),
				description = "hello im red",
				profile_picture = "no pic"
			});
			_userRepository.Setup(s => s.getUserById(3)).Returns(new UserModel()
			{
				user_id = 3,
				username = "christopher77",
				date_joined = new DateOnly(2001, 07, 25),
				description = "hello im chris",
				profile_picture = "no pic"
			});

			ShelfPageViewModel target = _shelfPageService.Object.createViewModel(id);
            Assert.Equal(target.name, exName);
            Assert.Equal(target.joinDate, exDate);
            Assert.Equal(target.description, exDesc);
            Assert.Equal(target.profilePicture, exPic);
        }

		[Fact]
		public void TEST_FUNCTION_FOR_FORMAT_BOOKS_FUNCTION()
		{

			List<AuthorModel> expectedAuthor1 = new List<AuthorModel>() {
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
				},
				new AuthorModel()
				{
					author_id = 3,
					first_name = "red",
					middle_name = "is",
					last_name = "green",
					full_name = "red is green"
				}
			};
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
			List<AuthorModel> expectedAuthor3 = new List<AuthorModel>() {
				new AuthorModel()
				{
					author_id = 1,
					first_name = "blare",
					middle_name = "witch",
					last_name = "craft",
					full_name = "blare witch craft"
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
			List<GenreModel> expectedGenre2 = new List<GenreModel>()
			{
				new GenreModel()
				{
					genre_id = 1,
					genre_name = "SCIFI"
				}
			};
			List<GenreModel> expectedGenre3 = new List<GenreModel>()
			{
				new GenreModel()
				{
					genre_id = 2,
					genre_name = "FANTASY"
				}
			};

			_authorService.Setup(s => s.createAuthorModelBatch(1)).Returns(expectedAuthor1);
			_authorService.Setup(s => s.createAuthorModelBatch(2)).Returns(expectedAuthor2);
			_authorService.Setup(s => s.createAuthorModelBatch(3)).Returns(expectedAuthor3);

			_genreService.Setup(s => s.createGenreModelBatch(1)).Returns(expectedGenre1);
			_genreService.Setup(s => s.createGenreModelBatch(2)).Returns(expectedGenre2);
			_genreService.Setup(s => s.createGenreModelBatch(3)).Returns(expectedGenre3);

			_bookService.Setup(s => s.getBookModelFromUser(1)).Returns(new List<BookModel>() {
				new BookModel() {
					book_id = 1,
					title = "red",
					pages = 2000,
					isbn = "redisbn",
					rating = 2,
					cover_picture = "redpic"
				},
				new BookModel() {
					book_id = 2,
					title = "blue",
					pages = 200,
					isbn = "blueisbn",
					rating = 5,
					cover_picture = "bluepic"
				},
				new BookModel() {
					book_id = 3,
					title = "green",
					pages = 500,
					isbn = "greenisbn",
					rating = 9,
					cover_picture = "greenpic"
				}
			});

			_userRepository.Setup(s => s.getUserById(1)).Returns(new UserModel()
			{
				user_id = 3,
				username = "christopher77",
				date_joined = new DateOnly(2001, 07, 25),
				description = "hello im chris",
				profile_picture = "no pic"
			});

			ShelfPageViewModel target = _shelfPageService.Object.createViewModel(1);
			Assert.Equal("christopher77", target.name);
			Assert.Equal(new DateOnly(2001, 07, 25), target.joinDate);
			Assert.Equal("hello im chris", target.description);
			Assert.Equal("no pic", target.profilePicture);
			Assert.Equal(new BookModel {
				book_id = 1,
				title = "red",
				pages = 2000,
				isbn = "redisbn",
				rating = 2,
				cover_picture = "redpic",
				authors = expectedAuthor1,
				genres = expectedGenre1
			},target.books.ElementAt(0));
			Assert.Equal(new BookModel
			{
				book_id = 2,
				title = "blue",
				pages = 200,
				isbn = "blueisbn",
				rating = 5,
				cover_picture = "bluepic",
				authors = expectedAuthor2,
				genres = expectedGenre2
			}, target.books.ElementAt(1));
			Assert.Equal(new BookModel
			{
				book_id = 3,
				title = "green",
				pages = 500,
				isbn = "greenisbn",
				rating = 9,
				cover_picture = "greenpic",
				authors = expectedAuthor3,
				genres = expectedGenre3
			}, target.books.ElementAt(2));
		}
    }
}
