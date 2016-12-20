using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public interface IFileSpecificationProvider
	{
		IEnumerable<string> Extensions { get; }

		UIImage GetThumbnail(NSUrl atUri, CGSize withSize);

		UIViewController GetViewControllerForItem(NSUrl atUri, NSData data, NSFileAttributes fileAttribute);
	}
}