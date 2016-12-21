using System;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class DirectoryViewController : UIViewController, IUISearchBarDelegate
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
			//searchController.SearchResultsUpdater = searchResultsViewController;

			directoryContentViewModel = viewModel.BuildDirectoryContentViewModel();
			directoryContentViewController = new DirectoryContentViewController(directoryContentViewModel);

			
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
			AddChildViewController(directoryContentViewController);
			NavigationItem.RightBarButtonItem = directoryContentViewController.NavigationItem.RightBarButtonItem;
			NavigationItem.Title = directoryContentViewController.NavigationItem.Title;
			Add(directoryContentViewController.View);
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
			OnFinish();
		}

		[Export("searchBarTextDidBeginEditing:")]
		public void DidBeginTextEditing(UISearchBar searchBar)
		{
			directoryContentViewController.SetEditing(false, true);
			searchResultsViewModel.SortMode = directoryContentViewModel.SortMode;
		}

		public event Action<Item<object>> ItemSelected;

		public event Action<Item<object>> ItemDetailsSelected;

		public event Action<Item<object>> ChooseItems;

		public event Action Finish;

		

		protected virtual void OnItemSelected(Item<object> obj)
		{
			ItemSelected?.Invoke(obj);
		}

		protected virtual void OnItemDetailsSelected(Item<object> obj)
		{
			ItemDetailsSelected?.Invoke(obj);
		}

		protected virtual void OnChooseItems(Item<object> obj)
		{
			ChooseItems?.Invoke(obj);
		}

		protected virtual void OnFinish()
		{
			Finish?.Invoke();
		}
	}
}