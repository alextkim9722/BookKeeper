using MainProject.Model;
using MainProject.Services.Abstracts;
using MainProject.Services.Interfaces;

namespace MainProject.Services
{
	public class GenreService : JoinServiceAbstract<Genre>, IGenreService
	{
		private readonly Callback handler1;

		public GenreService(BookShelfContext bookShelfContext) :
			base(bookShelfContext)
		{
			handler1 = addBridges;
			CallbackHandler += handler1;
		}

		public Genre? addGenre(Genre genre)
			=> addGenre(genre);

		public IEnumerable<Genre>? getAllGenres()
			=> formatAllModels();

		public Genre? getGenreById(int id)
			=> formatModel(CallbackHandler, null, x => x.genre_id == id);

		public Genre? getGenreByName(string name)
			=> formatModel(CallbackHandler, null, x => x.genre_name == name);

		public Genre? removeGenre(int id)
		{
			if (deleteBridges(id))
			{
				return deleteModel(x => x.genre_id == id);
			}
			else
			{
				return null;
			}
		}

		public Genre? updateGenre(int id, Genre genre)
		{
			Genre? updateGenre = updateModel(id, genre);
			
			if(updateGenre != null)
			{
				updateGenre.books = genre.books;
			}

			return updateGenre;
		}

		protected override Genre addBridges(Genre genre)
		{
			genre.books = getMultipleJoins<Book, Book_Genre>(
				x => x.genre_id == genre.genre_id, y => y.book_id);

			return genre;
		}

		protected override bool deleteBridges(int id)
		{
			return
				deleteJoins<Book_Genre>(x => x.genre_id == id);
		}
	}
}
