using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Model
{
	public class AuthorModel
	{
		[Key]
		public int author_id { get; set; }
		[Required]
		public string first_name { get; set; }
		public string middle_name { get; set; }
		[Required]
		public string last_name { get; set; }

		#region NON MAPPED PROPERTIES
		[NotMapped]
		public string full_name { get; set; }
		#endregion
	}
}
