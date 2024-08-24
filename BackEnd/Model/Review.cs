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
		[StringLength(300)]
		public string? description { get; set; }
		[Required]
		[Range(0, 10)]
		public int rating { get; set; }
		[Required]
		public DateOnly? date_submitted { get; set; } = new DateOnly();
	}
}
