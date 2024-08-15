using System.ComponentModel.DataAnnotations;

namespace MainProject.Model
{
	public class Genre
	{
		[Key]
		public int genre_id { get; set; }
		[Required]
		public string genre_name {  get; set; }
	}
}
