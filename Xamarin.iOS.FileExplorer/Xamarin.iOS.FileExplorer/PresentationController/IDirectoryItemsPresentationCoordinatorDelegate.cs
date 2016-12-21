using System.Collections.Generic;
using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.PresentationController
{
	public interface IDirectoryItemsPresentationCoordinatorDelegate
	{
		void ItemSelected(DirectoryItemPresentationCoordinator coordinator, Item<object> selectedItem);
		void ItemDetailsSelected(DirectoryItemPresentationCoordinator coordinator, Item<object> selectedItemDetails);

		void ItemsPicked(DirectoryItemPresentationCoordinator coordinator, IEnumerable<Item<object>> pickedItems);

		void Finished(DirectoryItemPresentationCoordinator coordinator);
	}
}