using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
	public class Identification : IdentityUser
	{
		[NotMapped]
		public int user_id { get; set; }
	}
}
