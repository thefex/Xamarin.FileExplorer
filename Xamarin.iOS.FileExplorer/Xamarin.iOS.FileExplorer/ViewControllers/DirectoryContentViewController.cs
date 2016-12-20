using System;
using Foundation;
using UIKit;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class DirectoryContentViewController : UICollectionViewController
	{
		private UIToolbar toolbar;
		private NSLayoutConstraint toolbarBottomConstraint;
		bool isFirstLayout = true;

		public UICollectionViewFlowLayout FlowLayout => CollectionView?.CollectionViewLayout as UICollectionViewFlowLayout;

		public DirectoryContentViewController()
		{
		}

		public DirectoryContentViewController(NSCoder coder) : base(coder)
		{
		}

		protected DirectoryContentViewController(NSObjectFlag t) : base(t)
		{
		}

		protected internal DirectoryContentViewController(IntPtr handle) : base(handle)
		{
		}

		public DirectoryContentViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
		{
		}

		public DirectoryContentViewController(UICollectionViewLayout layout) : base(layout)
		{
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();
			if ( FlowLayout == null)
				return;

		//	FlowLayout.ItemSize
		}
	}
}