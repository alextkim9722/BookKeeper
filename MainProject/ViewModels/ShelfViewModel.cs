namespace MainProject.ViewModels
{
	public class ShelfViewModel
	{
		public string profilePicture { get; set; } = "images/TempProfilePic.jpg";
		public string name { get; set; } = "name";
		public int pagesRead { get; set; } = 0;
		public int booksRead { get; set; } = 0;
		public DateOnly? joinDate { get; set; } = null;
		public string description { get; set; } = "empty description";
	}
}
