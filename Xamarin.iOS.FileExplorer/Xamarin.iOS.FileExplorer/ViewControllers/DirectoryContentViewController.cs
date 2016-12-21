using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.CustomViews;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Extensions;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
    public interface IDirectoryContentViewControllerDelegate
    {
        void ChangedEditingStatus(DirectoryContentViewController controller, bool isEditing);
        void SelectedItem(DirectoryContentViewController controller, Item<object> item);
        void SelectedItemDetails(DirectoryContentViewController controller, Item<object> item);
        void ChoosedItems(DirectoryContentViewController controller, Item<object> item);
    }

    public class DirectoryContentViewController : UICollectionViewController, 
                                                  IUISearchResultsUpdating
    {
        private const string Itemcellidentifier = "ItemCellIdentifier";
        private readonly DirectoryContentViewModel viewModel;
        private UIToolbar toolbar;
        private NSLayoutConstraint toolbarBottomConstraint;
        bool isFirstLayout = true;
        private UICollectionViewExtended CollectionViewExtended;

        public UICollectionViewFlowLayout FlowLayout => CollectionView?.CollectionViewLayout as UICollectionViewFlowLayout;

        public DirectoryContentViewController(DirectoryContentViewModel viewModel) :
            base(new UICollectionViewFlowLayout { ItemSize = new CGSize(200, 64), MinimumLineSpacing = 0 })
        {
            this.viewModel = viewModel;
            toolbar = UiToolbarExtensions.MakeToolbar();
            CollectionViewExtended = new UICollectionViewExtended(CollectionView.Frame, CollectionView.CollectionViewLayout);
            CollectionView = CollectionViewExtended;
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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (FlowLayout == null)
            {
                return;
            }
            ExtendedLayoutIncludesOpaqueBars = false;
            EdgesForExtendedLayout = UIRectEdge.None;

            CollectionViewExtended.BackgroundColor = UIColor.White;
            CollectionViewExtended.RegisterCell<ItemCell>();
            // TODO
            // CollectionViewExtended.RegisterHeader<CollectionViewHeader>();
             CollectionViewExtended.RegisterFooter<CollectionViewFooter>();
            CollectionViewExtended.AlwaysBounceVertical = true;
            CollectionViewExtended.AllowsMultipleSelection = true;
            CollectionViewExtended.AddSubview(toolbar);
            toolbarBottomConstraint = toolbar.PinToBottom(View);
            if (toolbarBottomConstraint != null)
            {
                toolbarBottomConstraint.Constant = toolbar.Bounds.Height;
            }
            SyncWithViewModel(false);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            if (FlowLayout == null)
            {
                return;
            }
            FlowLayout.ItemSize = new CGSize(View.Bounds.Width, 64);
            FlowLayout.HeaderReferenceSize = new CGSize(View.Bounds.Width, 44f);
            FlowLayout.FooterReferenceSize = new CGSize(View.Bounds.Width,
                CollectionViewExtended.Frame.Height - viewModel.NumberOfItems(0) * FlowLayout.ItemSize.Height);
            if (isFirstLayout)
            {
                isFirstLayout = false;
                CollectionView.ContentOffset = new CGPoint(CollectionView.ContentOffset.X, 44);
            }
        }

        private void SyncWithViewModel(bool animated)
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
                editBarButtonItem.Clicked += (sender, args) =>
                {
                    // TODO
                };
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
                selectActionButton.Clicked += (sender, args) =>
                {
                    //TODO
                };
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
                deleteActionButton.Clicked += (sender, args) =>
                {
                    // TODO
                };
            }
            toolbar.Items = new[]
            {
                selectActionButton,
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null, null),
                deleteActionButton
            };
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
        }
    }
}