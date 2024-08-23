using BackEnd.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
    [PrimaryKey(nameof(firstKey), nameof(secondKey))]
	public class Book_Genre : IDoublePKModel
	{
		[Required]
		[Column("BOOK_ID")]
		public int firstKey { get; set; }
		[Required]
		[Column("GENRE_ID")]
		public int secondKey { get; set; }
	}
}
