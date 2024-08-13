using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Model
{
	public class UserModel
	{
		[Key]
		public int user_id { get; set; }
		[Required]
		public string username { get; set; } = string.Empty;
		[Required]
		public DateOnly? date_joined { get; set; } = null;
		[Required]
		public string description { get; set; } = string.Empty;
		public string profile_picture { get; set; } = string.Empty;

		#region NON MAPPED PROPERTIES
		[NotMapped]
		public int pagesRead { get; set; }
		[NotMapped]
		public int booksRead { get; set; }
		#endregion
	}
}
