using System;
using Foundation;
using UIKit;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class ImageViewController : UIViewController
	{
		public ImageViewController(UIImage nibName)
		{
		}

		public ImageViewController(NSCoder coder) : base(coder)
		{
		}

		protected ImageViewController(NSObjectFlag t) : base(t)
		{
		}

		protected internal ImageViewController(IntPtr handle) : base(handle)
		{
		}

		public ImageViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
		{
		}
	}
}