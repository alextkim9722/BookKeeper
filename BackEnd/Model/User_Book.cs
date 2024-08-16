using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
	[PrimaryKey(nameof(user_id), nameof(book_id))]
	public class User_Book
	{
		public int user_id { get; set; }

		public int book_id { get; set; }
	}
}
