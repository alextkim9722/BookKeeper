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
		public IEnumerable<int> books { get; set; } = new List<int>();
		[NotMapped]
		public IEnumerable<int> reviews { get; set; } = new List<int>();
		#endregion
	}
}
