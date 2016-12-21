using System.Collections.Generic;
using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.PresentationController
{
	public interface IItemsPresentationCoordinatorDelegate
	{
		void Finished(ItemPresentationCoordinator coordinator);

		void ItemsPicked(ItemPresentationCoordinator coordinator, IEnumerable<Item<object>> pickedItems);
	}
}