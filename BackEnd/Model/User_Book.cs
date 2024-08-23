using BackEnd.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
    [PrimaryKey(nameof(firstKey), nameof(secondKey))]
	public class User_Book : IDoublePKModel
	{
		[Column("USER_ID")]
		public int firstKey { get; set; }
        [Column("BOOK_ID")]
        public int secondKey { get; set; }
	}
}
