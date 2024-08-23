using BackEnd.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Model
{
    [PrimaryKey(nameof(firstKey), nameof(secondKey))]
	public class Book_Author : IDoublePKModel
	{
		[Column("BOOK_ID")]
		public int firstKey { get; set; }
        [Column("AUTHOR_ID")]
        public int secondKey { get; set; }
	}
}
