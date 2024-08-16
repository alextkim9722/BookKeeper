using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
	[PrimaryKey(nameof(user_id), nameof(book_id))]
	public class Review
	{
		public int book_id { get; set; }
		public int user_id { get; set; }
		public string? description { get; set; }
		[Required]
		public int rating { get; set; }
		[Required]
		public DateOnly? date_submitted { get; set; } = new DateOnly();
	}
}
