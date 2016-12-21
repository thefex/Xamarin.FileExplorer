using Foundation;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public interface IFileExplorerViewControllerDelegate
	{
		void Finished(FileExplorerViewController vc);

		void ChoosedURLs(FileExplorerViewController vc, NSUrl[] pickedUrls);
	}
}