using System.ComponentModel.DataAnnotations;

namespace MainProject.Model
{
	public class UserModel
	{
		public int id { get; set; }
		[Required]
		public string name { get; set; } = string.Empty;
		[Required]
		public string email { get; set; } = string.Empty;
		[Required]
		public DateOnly? dateJoined { get; set; } = null;
		public string description { get; set; } = string.Empty;
		[Required]
		public int booksRead { get; set; } = 0;
		[Required]
		public int pagesRead { get; set; } = 0;
	}
}
