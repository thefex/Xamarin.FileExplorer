using System;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class DirectoryContentViewController : UICollectionViewController
	{
		private readonly DirectoryContentViewModel _viewModel;
		private UIToolbar toolbar;
		private NSLayoutConstraint toolbarBottomConstraint;
		bool isFirstLayout = true;

		public UICollectionViewFlowLayout FlowLayout => CollectionView?.CollectionViewLayout as UICollectionViewFlowLayout;

		public DirectoryContentViewController(DirectoryContentViewModel viewModel)
		{
			_viewModel = viewModel;
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