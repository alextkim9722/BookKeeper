using System.ComponentModel.DataAnnotations;

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
	}
}
