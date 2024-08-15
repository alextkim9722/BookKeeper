using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Model
{
	[PrimaryKey(nameof(book_id), nameof(author_id))]
	public class Book_Author
	{
		public int book_id { get; set; }
		
		public int author_id { get; set; }
	}
}
