using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.CustomViews;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public class UICollectionViewExtended : UICollectionView
	{
		public UICollectionViewExtended(NSCoder coder) : base(coder)
		{
		}

		public UICollectionViewExtended(NSObjectFlag t) : base(t)
		{
		}

		public UICollectionViewExtended(IntPtr handle) : base(handle)
		{
		}

		public UICollectionViewExtended(CGRect frame, UICollectionViewLayout layout) : base(frame, layout)
		{
		}

		private bool _isEditing;
		public bool IsEditing
		{
			get { return _isEditing; }
			set { SetEditing(value, false); }
		}

		public UIToolbar Toolbar { get; set; }

		public NSLayoutConstraint ToolbarBottomConstraint { get; set; }

		public void SetEditing(bool editing, bool animated)
		{
			foreach (var cell in VisibleCells.Where(x => x is IEditable).Cast<IEditable>())
				cell.SetEditing(editing, animated);
			_isEditing = editing;
		}
	}
}