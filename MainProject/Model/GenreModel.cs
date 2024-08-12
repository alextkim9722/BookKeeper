using System.ComponentModel.DataAnnotations;

namespace MainProject.Model
{
	public class GenreModel
	{
		[Key]
		public int genre_id { get; set; }
		[Required]
		public string genre_name {  get; set; }
	}
}
