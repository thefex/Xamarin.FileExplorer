using System.Linq;
using UIKit;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class UISearchBarExtensions
	{
		const int DimmingViewConstantId = -12312412;

		public static void SetEnabled(this UISearchBar searchBar, bool isEnabled)
		{
			bool isCurrentlyEnabled = searchBar.UserInteractionEnabled;

			if (isCurrentlyEnabled == isEnabled)
				return;

			if (isEnabled)
			{
				var dimmingView = searchBar.Subviews.FirstOrDefault(x => x.Tag == DimmingViewConstantId);
				dimmingView?.RemoveFromSuperview();
			}
			else
			{
				var dimmingView = new UIView()
				{
					Tag = DimmingViewConstantId,
					AutoresizingMask = UIViewAutoresizing.FlexibleDimensions,
					BackgroundColor = UIColor.Black.ColorWithAlpha(0.15f)
				};
				searchBar.Add(dimmingView);
			}

			searchBar.UserInteractionEnabled = isEnabled;
		}
	}
}