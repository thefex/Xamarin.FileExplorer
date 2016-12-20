namespace Xamarin.iOS.FileExplorer.Config
{
	public class ActionsConfiguration
	{
		public bool CanRemoveFiles { get; set; } = false;

		public bool CanRemoveDirectories { get; set; } = false;

		public bool CanChooseFiles { get; set; } = false;

		public bool CanChooseDirectories { get; set; } = false;

		public bool AllowsMultipleSelection { get; set; } = false;
	}
}