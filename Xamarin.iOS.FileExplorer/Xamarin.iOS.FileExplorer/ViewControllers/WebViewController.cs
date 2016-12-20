using System;
using CoreGraphics;
using Foundation;
using UIKit;
using WebKit;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public sealed class WebViewController : UIViewController
	{
		private readonly NSUrl _url;

		public WebViewController(NSUrl url) 
		{
			_url = url;
		}

		public WebViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var webView = new WKWebView(new CGRect(), new WKWebViewConfiguration());
			webView.TranslatesAutoresizingMaskIntoConstraints = false;
			View.Add(webView);
			webView.LoadFileUrl(_url, _url);
		}
	}
}
