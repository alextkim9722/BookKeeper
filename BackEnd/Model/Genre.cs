using BackEnd.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
	public class Genre
	{
		[Key]
		public int genre_id { get; set; }
		[Required]
		public string genre_name {  get; set; } = string.Empty;

		[NotMapped]
		public IEnumerable<Book>? books { get; set; }
	}
}
