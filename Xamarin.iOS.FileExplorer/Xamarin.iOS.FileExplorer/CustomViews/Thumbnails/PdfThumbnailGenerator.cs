using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews.Thumbnails
{
	public class PdfThumbnailGenerator : IThumbnailGenerator
	{
		public PdfThumbnailGenerator(NSUrl url)
		{
			Url = url;
		}


		public NSUrl Url { get; }

		public UIImage Generate(CGSize size)
		{
			var document = new CGPDFDocument(new CGDataProvider(Url));
			var page = document.GetPage(1);

			var orginalPageRect = page.GetBoxRect(CGPDFBox.Media);
			//var targetPageRect = A
			throw new NotImplementedException();
		}
	}
}