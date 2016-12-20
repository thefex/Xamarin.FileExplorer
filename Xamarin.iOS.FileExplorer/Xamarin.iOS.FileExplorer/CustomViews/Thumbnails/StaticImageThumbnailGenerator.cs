using CoreGraphics;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews.Thumbnails
{
	public class StaticImageThumbnailGenerator : IThumbnailGenerator
	{
		public StaticImageThumbnailGenerator(UIImage image)
		{
			Image = image;
		}

		public UIImage Image { get; }

		public UIImage Generate(CGSize size)
			=> Image;
	}
}