using System;
using System.Collections.Generic;
using System.Linq;
using CoreFoundation;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Extensions;
using Xamarin.iOS.FileExplorer.PresentationController;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class FileExplorerViewController : UIViewController
	{
		public Uri InitialDirectoryOfUrl { get; set; } = NSUrlExtensions.DocumentDirectory;

		public bool CanRemoveFiles { get; set; } = true;

		public bool CanRemoveDirectories { get; set; } = true;

		public bool CanChooseFiles { get; set; } = true;

		public bool CanChooseDirectories { get; set; } = false;

		public bool AllowsMultipleSelection { get; set; } = true;

		public IEnumerable<string> FileFilters { get; set; } = Enumerable.Empty<string>();

		public IEnumerable<string> IgnoredFileFilters { get; set; } = Enumerable.Empty<string>();

		public IEnumerable<IFileSpecificationProvider> CustomSpecificationProviders { get; }

		private ItemPresentationCoordinator itemPresentationCoordinator;

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
			AddChildViewController(navigationController);
			itemPresentationCoordinator = new ItemPresentationCoordinator(navigationController);
			
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
		}

		public event Action Finished;

		protected virtual void OnFinished()
		{
			Finished?.Invoke();
		}

		public event Action<IEnumerable<NSUrl>> FilesChoosed;

		protected virtual void OnFilesChoosed(IEnumerable<NSUrl> obj)
		{
			FilesChoosed?.Invoke(obj);
		}
	}
}