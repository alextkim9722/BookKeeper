using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
	public class Author
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int author_id { get; set; }
		[Required]
		public string first_name { get; set; } = string.Empty;
		public string middle_name { get; set; } = string.Empty;
		[Required]
		public string last_name { get; set; } = string.Empty;

		#region NON MAPPED PROPERTIES
		[NotMapped]
		public string full_name { get; set; } = string.Empty;
		[NotMapped]
		public IEnumerable<Book>? books { get; set; }
		#endregion
	}
}
