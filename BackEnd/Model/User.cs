using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Model;

namespace BackEnd.Model
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int user_id { get; set; }
		[Required]
		public string identification_id { get; set; }
		[Required]
		public string username { get; set; } = string.Empty;
		[Required]
		public DateOnly? date_joined { get; set; } = null;
		[Required]
		public string description { get; set; } = string.Empty;
		public string profile_picture { get; set; } = string.Empty;

		#region NON MAPPED PROPERTIES
		[NotMapped]
		public int? pagesRead { get; set; }
		[NotMapped]
		public int? booksRead { get; set; }
		[NotMapped]
		public IEnumerable<Book>? books { get; set; }
		[NotMapped]
		public IEnumerable<Review>? reviews { get; set; }
		#endregion
	}
}
