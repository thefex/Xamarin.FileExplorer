using System;
using System.Collections.Generic;
using System.Linq;
using CoreFoundation;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.Extensions;

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

		public FileExplorerViewController(NSUrl directoryUrl)
		{
			
		}
	}
}