using System;
using System.Collections.Generic;
using System.Linq;
using CoreFoundation;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Extensions;
using Xamarin.iOS.FileExplorer.PresentationController;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class FileExplorerViewController : UIViewController, IItemsPresentationCoordinatorDelegate
	{
		public Uri InitialDirectoryOfUrl { get; set; } = NSUrlExtensions.DocumentDirectory;

		public bool CanRemoveFiles { get; set; } = true;

		public bool CanRemoveDirectories { get; set; } = true;

		public bool CanChooseFiles { get; set; } = true;

		public bool CanChooseDirectories { get; set; } = false;

		public bool AllowsMultipleSelection { get; set; } = true;

		public IEnumerable<Filter> FileFilters { get; set; } = Enumerable.Empty<Filter>();

		public IEnumerable<Filter> IgnoredFileFilters { get; set; } = Enumerable.Empty<Filter>();

		public IEnumerable<IFileSpecificationProvider> CustomSpecificationProviders { get; }

		private ItemPresentationCoordinator itemPresentationCoordinator;

		public FileExplorerViewController() : this(NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).First())
		{
			
		}

		public FileExplorerViewController(NSUrl directoryUrl) : this(directoryUrl, Enumerable.Empty<IFileSpecificationProvider>())
		{
			
		}

		public FileExplorerViewController(NSUrl directoryUrl, IEnumerable<IFileSpecificationProvider> fileSpecificationProviders)
		{
			InitialDirectoryOfUrl = directoryUrl;
			CustomSpecificationProviders = fileSpecificationProviders;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			var navigationController = new UINavigationController();
			this.AddContentChildViewController(navigationController, new UIEdgeInsets());
			itemPresentationCoordinator = new ItemPresentationCoordinator(navigationController)
			{
				Delegate = this
			};
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			var fileSpecifications = new FileSpecifications();

			var actionsConfiguration = new ActionsConfiguration()
			{
				CanRemoveFiles = CanRemoveFiles,
				CanRemoveDirectories = CanRemoveDirectories,
				CanChooseDirectories = CanChooseDirectories,
				CanChooseFiles = CanChooseFiles,
				AllowsMultipleSelection = AllowsMultipleSelection
			};

			var filteringConfiguration = new FilteringConfiguration()
			{
				FileFilters = FileFilters,
				IgnoredFileFilters = IgnoredFileFilters
			};

			var configuration = new Configuration()
			{
				ActionsConfiguration = actionsConfiguration,
				FilteringConfiguration = filteringConfiguration
			};

			var itemToExplore = Item<object>.At(InitialDirectoryOfUrl, isDirectory: true);

			if (itemToExplore != null)
				itemPresentationCoordinator.Start(itemToExplore, fileSpecifications, configuration, false);
			else
				throw new InvalidOperationException("Passed url is incorrect.");
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			itemPresentationCoordinator.Stop(false);
		}

		public void Finished(ItemPresentationCoordinator coordinator)
		{
			DismissViewController(true, null);
			Delegate?.Finished(this);
		}

		public void ItemsPicked(ItemPresentationCoordinator coordinator, IEnumerable<Item<object>> pickedItems)
		{
			DismissViewController(true, null);
			Delegate?.ChoosedURLs(this, pickedItems.Select(x => x.Url).ToArray());
		}

		public IFileExplorerViewControllerDelegate Delegate { get; set; }
	}
}