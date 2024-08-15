using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Model
{
	[PrimaryKey(nameof(book_id), nameof(genre_id))]
	public class Book_Genre
	{
		public int book_id { get; set; }
		
		public int genre_id { get; set; }
	}
}
