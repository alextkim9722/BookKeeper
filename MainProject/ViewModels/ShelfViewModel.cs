namespace MainProject.ViewModels
{
	public class ShelfViewModel
	{
		public string profilePicture { get; set; } = "images/TempProfilePic.jpg";
		public string name {  get; set; }
		public int pagesRead {  get; set; }
		public int booksRead {  get; set; }
		public DateOnly joinDate { get; set; }
		public string description {  get; set; }
	}
}
