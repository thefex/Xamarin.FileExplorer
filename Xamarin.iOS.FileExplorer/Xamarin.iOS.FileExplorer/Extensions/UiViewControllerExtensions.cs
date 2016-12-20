using UIKit;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class UiViewControllerExtensions
	{
		public static void ShowLoadingIndicator(this UIViewController controller)
		{
			
		}

		public static void HideLoadingIndicator(this UIViewController controller)
		{
			
		}

		public static UIBarButtonItem GetActiveRightBarButtonItem(this UIViewController viewController)
		{
			return GetActiveNavigationItem(viewController)?.RightBarButtonItem;
		}

		public static void SetActiveRightBarButtonItem(this UIViewController viewController, UIBarButtonItem item)
		{
			var navItem = GetActiveNavigationItem(viewController);

			if (viewController == null)
				return;

			if (viewController.NavigationItem != null)
				viewController.NavigationItem.RightBarButtonItem = item;

			if (navItem != null)
				navItem.RightBarButtonItem = item;
		}

		public static string GetActiveNavigationItemTitle(this UIViewController viewController)
			=> GetActiveNavigationItem(viewController)?.Title;

		public static void SetActiveNavigationItemTitle(this UIViewController viewController, string title)
		{
			if (viewController == null)
				return;

			if (viewController.NavigationItem != null)
				viewController.NavigationItem.Title = title;

			var navItem = GetActiveNavigationItem(viewController);

			if (navItem != null)
				navItem.Title = title;
		}

		private static UINavigationItem GetActiveNavigationItem(UIViewController viewController)
		{
			var topViewController = viewController?.NavigationController?.TopViewController;
			if (topViewController == null)
				return null;

			if (topViewController.NavigationItem != null)
				return topViewController.NavigationItem;

			return GetActiveNavigationItem(topViewController.ParentViewController);
		}
	}
}