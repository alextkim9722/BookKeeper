﻿using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.Services.Interfaces;
using MainProject.ViewModel;
using System.Linq;

namespace MainProject.Services
{
    public class ShelfPageService : IShelfPageService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly IBookService _bookService;

        public ShelfPageService(
            IUserRepository userRepository,
            IAuthorService authorService,
            IGenreService genreService,
            IBookService bookService
            )
        {
            _userRepository = userRepository;
            _authorService = authorService;
            _genreService = genreService;
            _bookService = bookService;
        }

        public ShelfPageViewModel createViewModel(int id)
        {
            User user = getUserByID(id);

			ShelfPageViewModel sh = new ShelfPageViewModel()
            {
                profilePicture = user.profile_picture,
                name = user.username,
                pagesRead = 0,
                booksRead = 0,
                joinDate = user.date_joined,
                description = user.description,
                books = formatBooks(id)
            };

            return sh;
        }

        private User getUserByID(int id)
            => _userRepository.getUserById(id);

        private IEnumerable<Book> formatBooks(int id)
        {
            throw new NotImplementedException();
		}
    }
}
