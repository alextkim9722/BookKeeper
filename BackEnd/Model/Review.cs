using BackEnd.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
	[PrimaryKey(nameof(firstKey), nameof(secondKey))]
	public class Review : IDoublePKModel
	{
		[Column("USER_ID")]
		public int firstKey { get; set; }
		[Column("BOOK_ID")]
		public int secondKey { get; set; }
		[StringLength(300, ErrorMessage = "Description length exceeds 300 characters!")]
		public string? description { get; set; }
		[Required]
		[Range(0, 10, ErrorMessage = "Rating must be between 0 and 10!")]
		public int rating { get; set; } = 0;
		[Required(ErrorMessage = "Reviews need a date!")]
		public DateOnly? date_submitted { get; set; }
	}
}
