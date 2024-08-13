using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Model
{
	public class BookModel
	{
		[Key]
		public int book_id {  get; set; }
		[Required]
		public string title { get; set; }
		[Required]
		public int pages { get; set; }
		[Required]
		public string isbn { get; set; }
		[Required]
		public int rating { get; set; }
		[Required]
		public string cover_picture { get; set; }

		#region NON MAPPED PROPERTIES
		[NotMapped]
		public IEnumerable<AuthorModel> authors { get; set; }
		[NotMapped]
		public IEnumerable<GenreModel> genres { get; set; }
		#endregion
	}
}
