using UIKit;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class UiToolbarExtensions
	{
		public static UIToolbar MakeToolbar()
		{
			var toolbar = new UIToolbar()
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			toolbar.SizeToFit();
			return toolbar;
		}
	}
}