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
		[Required]
		public string title { get; set; } = string.Empty;
		[Required]
		public int pages { get; set; }
		[Required]
		public string isbn { get; set; } = string.Empty;
		[Required]
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
