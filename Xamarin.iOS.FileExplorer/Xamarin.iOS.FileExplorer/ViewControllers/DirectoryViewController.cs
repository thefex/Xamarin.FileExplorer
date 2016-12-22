using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Extensions;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class DirectoryViewController : UIViewController, IUISearchBarDelegate, IDirectoryContentViewControllerDelegate
	{
		private DirectoryViewModel viewModel;

		private UISearchController searchController;
		private DirectoryContentViewController searchResultsViewController;
		private DirectoryContentViewModel searchResultsViewModel;

		private DirectoryContentViewController directoryContentViewController;
		private DirectoryContentViewModel directoryContentViewModel;


		public DirectoryViewController(DirectoryViewModel directoryViewModel)
        {
			viewModel = directoryViewModel;

			searchResultsViewModel = viewModel.BuildDirectoryContentViewModel();
			searchResultsViewController = new DirectoryContentViewController(searchResultsViewModel);
			searchController = new UISearchController(searchResultsViewController);
			searchController.SearchResultsUpdater = searchResultsViewController;

			directoryContentViewModel = viewModel.BuildDirectoryContentViewModel();
			directoryContentViewController = new DirectoryContentViewController(directoryContentViewModel);

			searchResultsViewController.Delegate = this;
			directoryContentViewController.Delegate = this;
		}

		public DirectoryViewController(NSCoder coder) : base(coder)
		{
		}

		protected DirectoryViewController(NSObjectFlag t) : base(t)
		{
		}

		protected internal DirectoryViewController(IntPtr handle) : base(handle)
		{
		}

		public DirectoryViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ExtendedLayoutIncludesOpaqueBars = false;
			EdgesForExtendedLayout = UIRectEdge.None;

			SetupSearchBarController();
			this.AddContentChildViewController(directoryContentViewController, new UIEdgeInsets(searchController.SearchBar.Bounds.Height, 0,0,0));
			NavigationItem.RightBarButtonItem = directoryContentViewController.NavigationItem.RightBarButtonItem;
			NavigationItem.Title = directoryContentViewController.NavigationItem.Title;
			View.SendSubviewToBack(directoryContentViewController.View);
			SetupLeftBarButtonItem();
		}

		private void SetupSearchBarController()
		{
			var searchBar = searchController.SearchBar;
			searchBar.SizeToFit();
			searchBar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			searchBar.Delegate = this;
			View.Add(searchBar);
			NavigationItem.RightBarButtonItems = directoryContentViewController.NavigationItem.RightBarButtonItems;
		}

		private void SetupLeftBarButtonItem()
		{
			if (!viewModel.FinishButtonHidden)
				NavigationItem.LeftBarButtonItem = new UIBarButtonItem(viewModel.FinishButtonTitle, UIBarButtonItemStyle.Plain,
					(e, a) =>
					{
						HandleFinishTapButton();
					});
		}

		public bool IsSearchControllerActive
		{
			get { return searchController.Active; }
			set { searchController.Active = value; }
		}

		private void HandleFinishTapButton()
		{
			Delegate?.Finished(this);
		}

		[Export("searchBarTextDidBeginEditing:")]
		public void DidBeginTextEditing(UISearchBar searchBar)
		{
			directoryContentViewController.SetEditing(false, true);
			searchResultsViewModel.SortMode = directoryContentViewModel.SortMode;
		}

		public IDirectoryViewControllerDelegate Delegate { get; set; }


		public void ChangedEditingStatus(DirectoryContentViewController controller, bool isEditing)
		{
			searchController.SearchBar.SetEnabled(!isEditing);
		}

		public void SelectedItem(DirectoryContentViewController controller, Item<object> item)
		{
			Delegate?.ItemSelected(item);
		}

		public void SelectedItemDetails(DirectoryContentViewController controller, Item<object> item)
		{
			Delegate?.ItemDetailsSelected(item);
		}

		public void ChoosedItems(DirectoryContentViewController controller, IEnumerable<Item<object>> items)
		{
			Delegate?.ItemsPicked(items);
		}

	}
}