using System;
using UIKit;
using Xamarin.iOS.FileExplorer.Extensions;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
    public interface IActionsViewControllerDelegate
    {
        void ActionsViewControllerDidRequestRemoval(ActionsViewController controller);
        void ActionsViewControllerDidRequestShare(ActionsViewController controller);
    }

public class ActionsViewController : UIViewController
	{
		private readonly UIViewController _contentViewController;
		private readonly UIToolbar _toolbar = new UIToolbar();

		public ActionsViewController(UIViewController contentViewController)
		{
			_contentViewController = contentViewController;
		}

		public ActionsViewController(IntPtr handle) : base(handle)
		{
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			ExtendedLayoutIncludesOpaqueBars = false;
			EdgesForExtendedLayout = UIRectEdge.None;

			Add(_toolbar);
			_toolbar.TranslatesAutoresizingMaskIntoConstraints = false;
			_toolbar.SizeToFit();
			_toolbar.PinToBottom(View);
			_toolbar.Items = new[]
			{
				new UIBarButtonItem(UIBarButtonSystemItem.Action, (e, a) => { OnShareButtonTapped(); }),
				new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, (e, a) => { }),
				new UIBarButtonItem(UIBarButtonSystemItem.Trash, (e, a) => { OnTrashButtonTapped(); })
			};

			AddChildViewController(_contentViewController);
			NavigationItem.Title = _contentViewController.NavigationItem.Title;
		}

        public IActionsViewControllerDelegate ActionsViewControllerDelegate { get; set; }

		protected virtual void OnShareButtonTapped()
		{
		    ActionsViewControllerDelegate?.ActionsViewControllerDidRequestShare(this);
		}

		protected virtual void OnTrashButtonTapped()
		{
            ActionsViewControllerDelegate?.ActionsViewControllerDidRequestRemoval(this);
        }
    }
}