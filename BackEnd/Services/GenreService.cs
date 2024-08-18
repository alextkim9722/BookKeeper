using BackEnd.Model;
using BackEnd.Services.Abstracts;
using BackEnd.Services.ErrorHandling;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
	public class GenreService : JoinServiceAbstract<Genre>, IGenreService
	{
		public GenreService(BookShelfContext bookShelfContext) :
			base(bookShelfContext)
		{
			CallbackHandler.Add(addBridges);
		}

		public Results<Genre> addGenre(Genre genre)
			=> addModel(genre);

		public Results<IEnumerable<Genre>> getAllGenres()
			=> formatAllModels();

		public Results<Genre> getGenreById(int id)
			=> formatModel(x => x.genre_id == id);

		public Results<Genre> getGenreByName(string name)
			=> formatModel(x => x.genre_name == name);

		public Results<Genre> removeGenre(int id)
			=> deleteBridgedModel(id, x => x.genre_id == id);

		public Results<Genre> updateGenre(int id, Genre genre)
			=> updateModel(x => x.genre_id == id, genre);

		protected override Results<Genre> addBridges(Genre genre)
		{
			var books = getMultipleJoins<Book, Book_Genre>(
				x => x.genre_id == genre.genre_id, y => y.book_id);

			if (books.success)
			{
				genre.books = books.payload;
				return new ResultsSuccessful<Genre>(genre);
			}
			else
			{
				return new ResultsFailure<Genre>(
					books.msg
					+ "Failed to grab bridge tables");
			}
		}

		protected override Results<Genre> deleteBridges(int id)
		{
			return
				deleteJoins<Book_Genre>(x => x.genre_id == id);
		}

		protected override Genre transferProperties(Genre original, Genre updated)
		{
			original.books = updated.books;
			return original;
		}

		protected override Results<Genre> validateProperties(Genre model)
		=> new ResultsSuccessful<Genre>(model);
	}
}
