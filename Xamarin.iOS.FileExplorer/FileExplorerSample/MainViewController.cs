using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.ViewControllers;

namespace FileExplorerSample
{
	public class MainViewController : UIViewController
	{
		public MainViewController()
		{
		}

		public MainViewController(NSCoder coder) : base(coder)
		{
		}

		protected MainViewController(NSObjectFlag t) : base(t)
		{
		}

		protected internal MainViewController(IntPtr handle) : base(handle)
		{
		}

		public MainViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			UIButton button = new UIButton(new CGRect(100, 100, 100, 50));
			button.SetTitle("show picker", UIControlState.Normal);
			button.BackgroundColor = UIColor.Blue;

			button.TouchUpInside += Button_TouchUpInside;

			View.BackgroundColor = UIColor.White;

			View.Add(button);
		}

		private void Button_TouchUpInside(object sender, EventArgs e)
		{
            var fileExplorer = new FileExplorerViewController();
			PresentViewController(fileExplorer, true, () => { });
		}
	}
}