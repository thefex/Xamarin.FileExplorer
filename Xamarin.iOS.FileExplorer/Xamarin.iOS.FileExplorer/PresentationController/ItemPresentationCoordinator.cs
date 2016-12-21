using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.PresentationController
{
	public class ItemPresentationCoordinator : IDirectoryItemsPresentationCoordinatorDelegate
	{
		private readonly UINavigationController _navigationController;
		private Configuration configuration;
		private FileSpecifications fileSpecifications;
		private IList<object> _coordinators = new List<object>();
		

		public ItemPresentationCoordinator(UINavigationController navigationController)
		{
			_navigationController = navigationController;
		}

		public IItemsPresentationCoordinatorDelegate Delegate { get; set; }

		public void Start(Item<object> item, FileSpecifications fileSpecifications, Configuration configuration, bool animated)
		{
			this.configuration = configuration;
			this.fileSpecifications = fileSpecifications;

			switch (item.Type)
			{
				case ItemType.Directory:
					var coordinator = new DirectoryItemPresentationCoordinator(_navigationController, fileSpecifications,
						configuration);
					coordinator.Delegate = this;
					coordinator.Start(item.Url, animated);
					_coordinators.Add(item);
					break;
				case ItemType.File:
					var fileCoordinator = new FileItemPresentationCoordinator(_navigationController, item, fileSpecifications);
					fileCoordinator.Start(animated);
					_coordinators.Add(fileCoordinator);
					break;

			}
		}

		public void Stop(bool animated)
		{
			_coordinators.Clear();
			_navigationController?.SetViewControllers(new UIViewController[] {}, animated);
		}

		public void ItemSelected(DirectoryItemPresentationCoordinator coordinator, Item<object> selectedItem)
		{
			Start(selectedItem, fileSpecifications, configuration, true);
		}

		public void ItemDetailsSelected(DirectoryItemPresentationCoordinator coordinator, Item<object> selectedItemDetails)
		{
			if (_navigationController == null)
				throw new InvalidOperationException("nav controller is null.");

			var newCoordinator = new FileItemPresentationCoordinator(_navigationController, selectedItemDetails, fileSpecifications);
			_coordinators.Add(newCoordinator);
			newCoordinator.StartDetailsPreview(true);
		}

		public void ItemsPicked(DirectoryItemPresentationCoordinator coordinator, IEnumerable<Item<object>> pickedItems)
		{
			Delegate?.ItemsPicked(this, pickedItems);
		}

		public void Finished(DirectoryItemPresentationCoordinator coordinator)
		{
			Delegate?.Finished(this);
		}
	}
}