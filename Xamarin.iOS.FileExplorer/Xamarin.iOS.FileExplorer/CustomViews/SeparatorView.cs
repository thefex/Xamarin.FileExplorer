using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews
{
	public class SeparatorView : UIView
	{
		public SeparatorView()
		{
		}

		public SeparatorView(NSCoder coder) : base(coder)
		{
		}

		protected SeparatorView(NSObjectFlag t) : base(t)
		{
		}

		protected internal SeparatorView(IntPtr handle) : base(handle)
		{
		}

		public SeparatorView(CGRect frame) : base(frame)
		{
		}

		public override CGSize SizeThatFits(CGSize size)
		{
			return new CGSize(size.Width, 1.0/UIScreen.MainScreen.Scale);
		}

		public override CGSize IntrinsicContentSize => new CGSize(double.MaxValue, 1.0/UIScreen.MainScreen.Scale);
	}
}