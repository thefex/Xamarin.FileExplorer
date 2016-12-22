using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.CustomViews;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Extensions;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
    public class DirectoryContentViewController : UICollectionViewController, 
                                                  IUISearchResultsUpdating,
                                                  IDirectoryContentViewModelDelegate
    {
        public IDirectoryContentViewControllerDelegate Delegate { get; set; }

        private readonly DirectoryContentViewModel viewModel;
        private UIToolbar toolbar;
        private NSLayoutConstraint toolbarBottomConstraint;
        bool isFirstLayout = true;
        private UICollectionViewExtended CollectionViewExtended;

        public UICollectionViewFlowLayout CollectionViewLayout
            => CollectionView?.CollectionViewLayout as UICollectionViewFlowLayout;

        public DirectoryContentViewController(DirectoryContentViewModel viewModel) :
            base(new UICollectionViewFlowLayout { ItemSize = new CGSize(200, 64), MinimumLineSpacing = 0 })
        {
            this.viewModel = viewModel;
            toolbar = UiToolbarExtensions.MakeToolbar();

        }

        public DirectoryContentViewController(NSCoder coder) : base(coder)
        {
        }

        public DirectoryContentViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
        }

        public DirectoryContentViewController(UICollectionViewLayout layout) : base(layout)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (CollectionViewLayout == null)
            {
                return;
            }
            ExtendedLayoutIncludesOpaqueBars = false;
            EdgesForExtendedLayout = UIRectEdge.None;

			CollectionViewExtended = new UICollectionViewExtended(CollectionView.Frame, CollectionView.CollectionViewLayout);
			CollectionView = CollectionViewExtended;

			CollectionViewExtended.BackgroundColor = UIColor.White;
            CollectionViewExtended.RegisterCell<ItemCell>();
            CollectionViewExtended.RegisterHeader<CollectionViewHeader>();
            CollectionViewExtended.RegisterFooter<CollectionViewFooter>();
            CollectionViewExtended.AlwaysBounceVertical = true;
            CollectionViewExtended.AllowsMultipleSelection = true;
            CollectionViewExtended.AddSubview(toolbar);
            toolbarBottomConstraint = toolbar.PinToBottom(View);
            if (toolbarBottomConstraint != null)
            {
                toolbarBottomConstraint.Constant = toolbar.Bounds.Height;
            }
            SyncWithViewModel();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            if (CollectionViewLayout == null)
            {
                return;
            }
            CollectionViewLayout.ItemSize = new CGSize(View.Bounds.Width, 64);
            CollectionViewLayout.HeaderReferenceSize = new CGSize(View.Bounds.Width, 44f);
            CollectionViewLayout.FooterReferenceSize = new CGSize(View.Bounds.Width,
                CollectionViewExtended.Frame.Height -
                viewModel.NumberOfItems(0) * CollectionViewLayout.ItemSize.Height);
            if (isFirstLayout)
            {
                isFirstLayout = false;
                CollectionView.ContentOffset = new CGPoint(CollectionView.ContentOffset.X, 44);
            }
        }

        private void SyncWithViewModel()
        {
            if (toolbar?.Items != null)
            {
                foreach (var item in toolbar.Items)
                {
                    item.Enabled = viewModel.IsDeleteActionEnabled;
                }
            }
            SyncToolbarWithViewModel();

            var editBarButtonItem =
                viewModel.IsEditActionHidden
                    ? null
                    : new UIBarButtonItem
                    {
                        Title = viewModel.IsEditActionTitle,
                        Style = UIBarButtonItemStyle.Plain,
                        Target = this
                    };
            if (editBarButtonItem != null)
            {
                editBarButtonItem.Clicked += HandleEditButtonTap;
                editBarButtonItem.Enabled = viewModel.IsEditActionEnabled;
            }
            this.SetActiveRightBarButtonItem(editBarButtonItem);
            this.SetActiveNavigationItemTitle(viewModel.Title);
            View.UserInteractionEnabled = viewModel.IsUserInteractionEnabled;
            SetEditing(viewModel.IsEditing, true);
        }

        public override void SetEditing(bool editing, bool animated)
        {
            base.SetEditing(editing, animated);
            if (CollectionViewExtended.IsEditing == editing)
            {
                return;
            }
            if (CollectionView?.GetIndexPathsForSelectedItems() != null)
            {
                foreach (var indexPath in CollectionView.GetIndexPathsForSelectedItems())
                {
                    CollectionView.DeselectItem(indexPath, animated);
                }
            }
            CollectionViewExtended.SetEditing(editing, animated);
            UIView.Animate(0.2f, () =>
            {
                toolbarBottomConstraint.Constant = editing ? 0 : toolbar.Bounds.Bottom;
                CollectionView.ContentInset = new UIEdgeInsets(CollectionView.ContentInset.Top,
                    CollectionView.ContentInset.Left, editing ? toolbar.Bounds.Height : 0.0f,
                    CollectionView.ContentInset.Right);
                CollectionView.ScrollIndicatorInsets = CollectionView.ContentInset;
                CollectionView.LayoutIfNeeded();
            });
        }

        private void SyncToolbarWithViewModel()
        {
            var selectActionButton = !viewModel.IsSelectActionHidden
                ? new UIBarButtonItem
                {
                    Title = viewModel.SelectActionTitle,
                    Style = UIBarButtonItemStyle.Plain,
                    Target = this,
                }
                : null;
            if (selectActionButton != null)
            {
                selectActionButton.Clicked += HandleSelectButtonTap;
                selectActionButton.Enabled = viewModel.IsSelectActionEnabled;
            }
            var deleteActionButton = !viewModel.IsDeleteActionHidden
                ? new UIBarButtonItem(UIBarButtonSystemItem.Action)
                {
                    Title = viewModel.DeleteActionTitle,
                    Style = UIBarButtonItemStyle.Plain,
                    Target = this
                }
                : null;
            if (deleteActionButton != null)
            {
                deleteActionButton.Enabled = viewModel.IsDeleteActionEnabled;
                deleteActionButton.Clicked += HandleDeleteButtonTap;
            }
            toolbar.Items = new[]
            {
                selectActionButton,
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null, null),
                deleteActionButton
            };
        }

        private void HandleDeleteButtonTap(object sender, EventArgs e)
        {
            this.ShowLoadingIndicator();
            viewModel.DeleteItems(viewModel.IndexPathsOfSelectedCells, result =>
            {
                this.HideLoadingIndicator();
                if (!result.IsSuccess)
                {
                    this.PresentAlert(result.ErrorMessage);
                }
                viewModel.IsEditing = false;
                Delegate?.ChangedEditingStatus(this, viewModel.IsEditing);
            });
        }

        private void HandleSelectButtonTap(object sender, EventArgs e)
        {
            viewModel.ChooseItems(selectedItems =>
            {
                Delegate?.ChoosedItems(this, selectedItems);
            });
        }

        private void HandleEditButtonTap(object sender, EventArgs e)
        {
            viewModel.IsEditing = !viewModel.IsEditing;
            Delegate?.ChangedEditingStatus(this, viewModel.IsEditing);
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            UpdateSearchResultsForSearchController(searchController);
            viewModel.SearchQuery = searchController.SearchBar.Text;
        }


        public void ListChanged(DirectoryContentViewModel viewModel)
	    {
		    CollectionView?.ReloadData();
	    }

	    public void Changed(DirectoryContentViewModel viewModel)
	    {
		    SyncWithViewModel();
	    }

	    public void ItemSelected(Item<object> selectedItem)
	    {
		    Delegate?.SelectedItem(this, selectedItem);
	    }

		public override nint NumberOfSections(UICollectionView collectionView)
		{
			return viewModel.NumberOfSections;
		}

		public override nint GetItemsCount(UICollectionView collectionView, nint section)
		{
			return viewModel.NumberOfItems((int)section);
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.DequeueReusableCell(typeof(ItemCell).ToString(), indexPath) as ItemCell;
			var itemViewModel = viewModel.ItemViewModelFor(indexPath);

			cell.TapAction = () =>
			{
				Delegate?.SelectedItemDetails(this, viewModel.ItemFor(indexPath));
			};
			cell.Selected = viewModel.IndexPathsOfSelectedCells.Any(x => x == indexPath);
			cell.Title = itemViewModel.Title;
			cell.Subtitle = itemViewModel.Subtitle;
			cell.Accessory = itemViewModel.Accessory;
			cell.IconImage = itemViewModel.GetThumbnailImage(cell.MaximumIconSize);
			return cell;
		}

		public override UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
		{
			if (elementKind == UICollectionElementKindSectionKey.Header)
			{
				var header = CollectionViewExtended.DequeueReusableHeader<CollectionViewHeader>(typeof(CollectionViewHeader), indexPath) as CollectionViewHeader;
				// sort mode
				UIView.PerformWithoutAnimation(() =>
				{
					header.LayoutIfNeeded();
				});
				return header;
			}
			else if (elementKind == UICollectionElementKindSectionKey.Footer)
			{
				return CollectionViewExtended.DequeueReusableFooter<CollectionViewFooter>(typeof(CollectionViewFooter), indexPath);
			}

			throw new InvalidOperationException("ElementKind is not registered.");
		}

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			viewModel.Select(indexPath);
			if (!viewModel.IsSelectionEnabled)
				collectionView.DeselectItem(indexPath, false);
		}

		public override void ItemDeselected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			viewModel.Deselect(indexPath);
		}
	}
}