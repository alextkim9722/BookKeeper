using BackEnd.Model.Interfaces;
using BackEnd.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
	public class Genre : ISinglePKModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("GENRE_ID")]
		public int pKey { get; set; }
		[Required]
		[StringLength(100, ErrorMessage = "Name length exceeds 100 characters!")]
		public string genre_name {  get; set; } = string.Empty;
		[NotMapped]
		public IEnumerable<int> books { get; set; } = new List<int>();
	}
}
