using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.ViewControllers;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public class DefaultFileSpecificationProvider : IFileSpecificationProvider
	{
		public IEnumerable<string> Extensions { get; } = new List<string>();
		public UIImage GetThumbnail(NSUrl atUri, CGSize withSize) => null;

		public UIViewController GetViewControllerForItem(NSUrl atUri, NSData data, NSFileAttributes fileAttribute)
		{
			return new UnknownFileTypeViewController(atUri.LastPathComponent);
		}
	}
}