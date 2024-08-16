using Microsoft.EntityFrameworkCore;

namespace BackEnd.Model
{
	[PrimaryKey(nameof(user_id), nameof(review_id))]
	public class User_Review
	{
		public int user_id { get; set; }
		public int review_id { get; set; }
	}
}
