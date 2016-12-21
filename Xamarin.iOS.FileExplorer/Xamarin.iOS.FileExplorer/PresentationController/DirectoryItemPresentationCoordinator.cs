using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;
using Xamarin.iOS.FileExplorer.ViewControllers;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.PresentationController
{
	public class DirectoryItemPresentationCoordinator : IDirectoryItemsPresentationCoordinatorDelegate
	{
		DirectoryViewController _directoryViewController;
 
		UINavigationController _navigationController;
		private readonly FileSpecifications _fileSpecifications;
		private readonly Configuration _configuration;
		private readonly IFileService<object> _fileService;

		UIViewController pushedViewController;

		public DirectoryItemPresentationCoordinator(UINavigationController controller, FileSpecifications fileSpecifications, Configuration configuration) : this(controller, fileSpecifications, configuration, new LocalStorageFileService<object>())
		{
			
		}

		public DirectoryItemPresentationCoordinator(UINavigationController controller, FileSpecifications fileSpecifications, Configuration configuration, IFileService<object> fileService)
		{
			_navigationController = controller;
			_fileSpecifications = fileSpecifications;
			_configuration = configuration;
			_fileService = fileService;
		}

		public void Start(NSUrl directoryUrl, bool animated)
		{
			bool finishButtonHidden = _navigationController?.ViewControllers?.Any() ?? false;

			if (directoryUrl.HasDirectoryPath)
			{
				var viewController = LoadingViewController<object>.Build(Item<object>.FromDirectory(directoryUrl), _fileService,
					loadedItem =>
					{
						var viewModel = new DirectoryViewModel(loadedItem.Url, loadedItem, _fileSpecifications, _configuration, finishButtonHidden);
						var directoryViewController = new DirectoryViewController(viewModel);
						// delegate
						_directoryViewController = directoryViewController;

						return directoryViewController;
					});

				_navigationController?.PushViewController(viewController, true);
			}
		}

		public IDirectoryItemsPresentationCoordinatorDelegate Delegate { get; set; }
		public void ItemSelected(DirectoryItemPresentationCoordinator coordinator, Item<object> selectedItem)
		{
			if (_directoryViewController != null)
				_directoryViewController.IsSearchControllerActive = false;
			Delegate?.ItemSelected(coordinator, selectedItem);
		}

		public void ItemDetailsSelected(DirectoryItemPresentationCoordinator coordinator, Item<object> selectedItemDetails)
		{
			if (_directoryViewController != null)
				_directoryViewController.IsSearchControllerActive = false;
			Delegate?.ItemDetailsSelected(coordinator, selectedItemDetails);
		}

		public void ItemsPicked(DirectoryItemPresentationCoordinator coordinator, IEnumerable<Item<object>> pickedItems)
		{
			Delegate?.ItemsPicked(coordinator, pickedItems);
		}

		public void Finished(DirectoryItemPresentationCoordinator coordinator)
		{
			Delegate?.Finished(coordinator);
		}
	}
}