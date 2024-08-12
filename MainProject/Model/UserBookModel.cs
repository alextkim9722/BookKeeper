using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Model
{
	[PrimaryKey(nameof(user_id), nameof(book_id))]
	public class UserBookModel
	{
		public int user_id { get; set; }

		public int book_id { get; set; }
	}
}
