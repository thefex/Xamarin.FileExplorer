using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.PresentationController;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public interface IDirectoryViewControllerDelegate
	{
		void ItemSelected(Item<object> selectedItem);

		void ItemDetailsSelected(Item<object> selectedItem);

		void ItemsPicked(Item<object> items);

		void Finished(DirectoryViewController directoryVc);
	}
}