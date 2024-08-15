using MainProject.Model;

namespace MainProject.ViewModel
{
	public class ShelfPageViewModel
	{
		public string profilePicture { get; set; } = string.Empty;
		public string name { get; set; } = string.Empty;
		public int pagesRead { get; set; } = 0;
		public int booksRead { get; set; } = 0;
		public DateOnly? joinDate { get; set; } = null;
		public string description { get; set; } = string.Empty;
		public IEnumerable<Book> books { get; set; } = null;
	}
}
