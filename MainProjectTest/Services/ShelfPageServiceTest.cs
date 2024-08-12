using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly Mock<ShelfPageService> _shelfPageGet;

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
			_shelfPageGet = new Mock<ShelfPageService>(
				_userRepository.Object,
				_authorService.Object,
				_bookService.Object,
				_genreService.Object);
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

			ShelfPageViewModel target = _shelfPageGet.Object.createViewModel(id);
            Assert.Equal(target.name, exName);
            Assert.Equal(target.joinDate, exDate);
            Assert.Equal(target.description, exDesc);
            Assert.Equal(target.profilePicture, exPic);
        }
    }
}
