using AVFoundation;
using CoreGraphics;
using CoreMedia;
using Foundation;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews.Thumbnails
{
	public class VideoThumbnailGenerator : IThumbnailGenerator
	{
		public VideoThumbnailGenerator(NSUrl url)
		{
			Url = url;
		}

		public NSUrl Url { get; }

		public UIImage Generate(CGSize size)
		{
			var asset = new AVUrlAsset(Url);
			var generator = new AVAssetImageGenerator(asset)
			{
				AppliesPreferredTrackTransform = true,
				MaximumSize = new CGSize(size.Width*UIScreen.MainScreen.Scale, size.Height*UIScreen.MainScreen.Scale)
			};

			var actualTime = new CMTime(0, 1000);
			NSError error = null;
			var cgImage = generator.CopyCGImageAtTime(new CMTime(1, 1000), out actualTime, out error);
			if (error == null)
				return null;

			return new UIImage(cgImage);
		}
	}
}