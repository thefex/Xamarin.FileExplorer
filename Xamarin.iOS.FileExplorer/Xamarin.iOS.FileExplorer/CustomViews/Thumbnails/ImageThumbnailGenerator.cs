using System;
using CoreGraphics;
using Foundation;
using ImageIO;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews.Thumbnails
{
	public sealed class ImageThumbnailGenerator : IThumbnailGenerator
	{
		public ImageThumbnailGenerator(NSUrl url)
		{
			Url = url;
		}

		public NSUrl Url { get; }

		public UIImage Generate(CGSize size)
		{
			try
			{
				var imageSource = CGImageSource.FromUrl(Url);

				var thumbnail = imageSource.CreateThumbnail(0,
					new CGImageThumbnailOptions
					{
						MaxPixelSize = (int) (Math.Max(size.Width, size.Height)*UIScreen.MainScreen.Scale),
						CreateThumbnailFromImageIfAbsent = true
					});
				return UIImage.FromImage(thumbnail);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}