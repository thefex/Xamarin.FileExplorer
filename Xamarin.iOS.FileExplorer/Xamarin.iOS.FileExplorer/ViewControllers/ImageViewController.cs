using System;
using Foundation;
using UIKit;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class ImageViewController : UIViewController, IUIScrollViewDelegate
	{
		private readonly UIImage _image;

		private UIScrollView scrollView;
		private UIImageView imageView;

		public ImageViewController(UIImage image)
		{
			_image = image;
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

		public override void LoadView()
		{
			base.LoadView();

			scrollView = new UIScrollView();
			View = scrollView;

			var uiImageView = new UIImageView(_image);
			uiImageView.Frame = View.Bounds;
			uiImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			uiImageView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			this.imageView = uiImageView;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ExtendedLayoutIncludesOpaqueBars = false;
			EdgesForExtendedLayout = UIRectEdge.None;

			scrollView.MinimumZoomScale = 0.5f;
			scrollView.MaximumZoomScale = 2f;
			scrollView.Delegate = this;
			scrollView.Add(imageView);
		}

		[Export("viewForZoomingInScrollView:")]
		public UIView ViewForZomming(UIScrollView sv) => imageView;

		[Export("scrollViewDidZoom:")]
		public void DidEndZooming(UIScrollView sv, UIView withView, nfloat atScale)
		{
			
		}
	}
}