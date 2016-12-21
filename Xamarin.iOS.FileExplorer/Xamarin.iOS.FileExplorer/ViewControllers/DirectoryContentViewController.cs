using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.CustomViews;
using Xamarin.iOS.FileExplorer.Extensions;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class DirectoryContentViewController : UICollectionViewController, IUISearchResultsUpdating
	{
	    private const string Itemcellidentifier = "ItemCellIdentifier";
	    private readonly DirectoryContentViewModel _viewModel;
		private UIToolbar toolbar;
		private NSLayoutConstraint toolbarBottomConstraint;
		bool isFirstLayout = true;

		public UICollectionViewFlowLayout FlowLayout => CollectionView?.CollectionViewLayout as UICollectionViewFlowLayout;

	    public DirectoryContentViewController(DirectoryContentViewModel viewModel) :
	        base(new UICollectionViewFlowLayout {ItemSize = new CGSize(200, 64), MinimumLineSpacing = 0})
	    {
	        _viewModel = viewModel;
	        toolbar = UiToolbarExtensions.MakeToolbar();
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
		    if (FlowLayout == null)
		    {
                return;
            }
		    ExtendedLayoutIncludesOpaqueBars = false;
            EdgesForExtendedLayout = UIRectEdge.None;
		    CollectionView.BackgroundColor = UIColor.White;
            CollectionView.RegisterClassForCell(typeof(ItemCell), Itemcellidentifier);
  	}

	    public void UpdateSearchResultsForSearchController(UISearchController searchController)
	    {
	        
	    }
	}
}