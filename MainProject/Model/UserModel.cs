using System.ComponentModel.DataAnnotations;

namespace MainProject.Model
{
	public class UserModel
	{
		public int id { get; set; }
		[Required]
		public string name { get; set; } = string.Empty;
		[Required]
		public DateOnly? dateJoined { get; set; } = null;
		public string description { get; set; } = string.Empty;
	}
}
