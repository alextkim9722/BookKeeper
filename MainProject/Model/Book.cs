using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Model
{
	public class Book
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int book_id {  get; set; }
		[Required]
		public string title { get; set; } = string.Empty;
		[Required]
		public int pages { get; set; }
		[Required]
		public string isbn { get; set; } = string.Empty;
		[Required]
		public int rating { get; set; }
		[Required]
		public string cover_picture { get; set; } = string.Empty;

		#region NON MAPPED PROPERTIES
		[NotMapped]
		public IEnumerable<Author>? authors { get; set; }
		[NotMapped]
		public IEnumerable<Genre>? genres { get; set; }
		[NotMapped]
		public int? readers { get; set; } = 0;
		#endregion
	}
}
