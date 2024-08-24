using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Model.Interfaces;

namespace BackEnd.Model
{
	public class Author : ISinglePKModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("AUTHOR_ID")]
		public int pKey { get; set; }
		[Required]
		[StringLength(Int32.MaxValue, ErrorMessage = "First name length exceeds maximum characters!")]
		public string first_name { get; set; } = string.Empty;
		[StringLength(Int32.MaxValue, ErrorMessage = "Middle name length exceeds maximum characters!")]
		public string middle_name { get; set; } = string.Empty;
		[Required]
		[StringLength(Int32.MaxValue, ErrorMessage = "Last name length exceeds maximum characters!")]
		public string last_name { get; set; } = string.Empty;

		#region NON MAPPED PROPERTIES
		[NotMapped]
		public string full_name { get; set; } = string.Empty;
		[NotMapped]
		public IEnumerable<int> books { get; set; } = new List<int>();
		#endregion
	}
}
