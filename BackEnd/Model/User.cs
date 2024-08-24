using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Model.Interfaces;

namespace BackEnd.Model
{
	public class User : ISinglePKModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("USER_ID")]
		public int pKey { get; set; }
		[Required]
		public string identification_id { get; set; } = string.Empty;
		[Required]
		[StringLength(25, ErrorMessage = "Name length exceeds 25 characters!")]
		public string username { get; set; } = string.Empty;
		[Required]
		public DateOnly? date_joined { get; set; } = null;
		[Required]
		[StringLength(300, ErrorMessage = "Description length exceeds 300 characters!")]
		public string description { get; set; } = string.Empty;
		public string profile_picture { get; set; } = string.Empty;

		#region NON MAPPED PROPERTIES
		[NotMapped]
		[Range(0, Int32.MaxValue, ErrorMessage = "Pages read exceeds maximum value!")]
		public int pagesRead { get; set; } = 0;
		[NotMapped]
		[Range(0, Int32.MaxValue, ErrorMessage = "Books read exceeds maximum value!")]
		public int booksRead { get; set; } = 0;
		[NotMapped]
		public IEnumerable<int> books { get; set; } = new List<int>();
		[NotMapped]
		public IEnumerable<int> reviews { get; set; } = new List<int>();
		#endregion
	}
}
