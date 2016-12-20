using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.ViewControllers;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public class PdfSpecificationProvider : IFileSpecificationProvider
	{
		public IEnumerable<string> Extensions { get; } = new List<string>() {"pdf"};
		public UIImage GetThumbnail(NSUrl atUri, CGSize withSize)
		{
			throw new NotImplementedException();
		}

		public UIViewController GetViewControllerForItem(NSUrl atUri, NSData data, NSFileAttributes fileAttribute)
		{
			return new WebViewController(atUri);
		}
	}
}