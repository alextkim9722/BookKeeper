using Microsoft.EntityFrameworkCore;

namespace BackEnd.Model
{
	[PrimaryKey(nameof(book_id), nameof(review_id))]
	public class Book_Review
	{
		public int book_id { get; set; }
		public int review_id { get; set; }
	}
}
