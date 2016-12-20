using CoreGraphics;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews.Thumbnails
{
	public interface IThumbnailGenerator
	{
		UIImage Generate(CGSize size);
	}
}