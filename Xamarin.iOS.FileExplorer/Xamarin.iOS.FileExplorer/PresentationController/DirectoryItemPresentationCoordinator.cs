using System.Linq;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Services.File;
using Xamarin.iOS.FileExplorer.ViewControllers;

namespace Xamarin.iOS.FileExplorer.PresentationController
{
	public class DirectoryItemPresentationCoordinator<T>
	{
		DirectoryViewController directoryViewController;
 
		UINavigationController _navigationController;
		private readonly FileSpecifications _fileSpecifications;
		private readonly Configuration _configuration;
		private readonly IFileService<T> _fileService;

		UIViewController pushedViewController;

		public DirectoryItemPresentationCoordinator(UINavigationController controller, FileSpecifications fileSpecifications, Configuration configuration) : this(controller, fileSpecifications, configuration, new LocalStorageFileService<T>())
		{
			
		}

		public DirectoryItemPresentationCoordinator(UINavigationController controller, FileSpecifications fileSpecifications, Configuration configuration, IFileService<T> fileService)
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
				//var viewController = 
			}
		}
	}
}