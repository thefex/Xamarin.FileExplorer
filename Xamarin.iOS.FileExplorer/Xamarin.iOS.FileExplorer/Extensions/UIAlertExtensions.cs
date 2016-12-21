using UIKit;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class UIAlertExtensions
	{
		public static void PresentAlert(this UIViewController viewController, string errorMessage)
		{
			var alertController = UIAlertController.Create("Error", errorMessage, UIAlertControllerStyle.Alert);
			alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			viewController.PresentViewController(alertController, true, null);
		}
	}
}