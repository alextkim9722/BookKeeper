using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Model.Interfaces;

namespace BackEnd.Model
{
	public class Book : ISinglePKModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("BOOK_ID")]
		public int pKey {  get; set; }
		[Required(ErrorMessage = "Title is required!")]
		[StringLength(Int32.MaxValue, ErrorMessage = "Title length exceeds maximum characters!")]
		public string title { get; set; } = string.Empty;
		[Required(ErrorMessage = "Page count is required!")]
		[Range(1, Int32.MaxValue, ErrorMessage = "Page length exceeds maximum value or is less than or equal to 0!")]
		public int pages { get; set; } = 0;
		[Required(ErrorMessage = "ISBN is required!")]
		[StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN length not within 13 digit standard!")]
		public string isbn { get; set; } = string.Empty;
		[Required(ErrorMessage = "Cover Picture is required!")]
		[StringLength(Int32.MaxValue, ErrorMessage = "Cover picture path length exceeds maximum characters!")]
		public string cover_picture { get; set; } = string.Empty;

		#region NON MAPPED PROPERTIES
		[NotMapped]
		public IEnumerable<int> authors { get; set; } = new List<int>();
		[NotMapped]
		public IEnumerable<int> genres { get; set; } = new List<int>();
		[NotMapped]
		public IEnumerable<int> reviews { get; set; } = new List<int>();
		[NotMapped]
		public IEnumerable<int> users { get; set; } = new List<int>();
		[NotMapped]
		public int rating { get; set; }
		#endregion
	}
}
