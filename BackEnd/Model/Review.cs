using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
	public class Review
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int review_id { get; set; }
		[Required]
		public string description { get; set; }
		[Required]
		public int rating { get; set; }
	}
}
