using System;
using System.IO;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.CustomViews
{
    public sealed class CollectionViewHeader : UICollectionReusableView
    {
        private UISegmentedControl segmentedControl;
        private readonly Action<SortMode> sortModeChangeAction = sortMode => { };

        public CollectionViewHeader()
        {
            Initialize();
        }

        public CollectionViewHeader(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public CollectionViewHeader(CGRect frame) : base(frame)
        {
            Initialize();
        }

        private void Initialize()
        {
            segmentedControl = new UISegmentedControl(new object[] {"Name", "Date"});
            segmentedControl.SizeToFit();
            segmentedControl.Frame = new CGRect(segmentedControl.Frame.X, segmentedControl.Frame.Y,
                160, segmentedControl.Bounds.Size.Height);
            segmentedControl.AutoresizingMask = UIViewAutoresizing.None;
            segmentedControl.ValueChanged += (sender, args) =>
            {
                sortModeChangeAction(sortMode);
            };
            AddSubview(segmentedControl);
            sortMode = SortMode.Name;
        }

        private SortMode sortMode
        {
            get
            {
                switch (segmentedControl.SelectedSegment)
                {
                    case 0:
                        return SortMode.Name;
                    case 1:
                        return SortMode.Date;
                    default:
                        throw new InvalidDataException();
                }
            }
            set
            {
                switch (value)
                {
                    case SortMode.Name:
                        segmentedControl.SelectedSegment = 0;
                        break;
                    case SortMode.Date:
                        segmentedControl.SelectedSegment = 1;
                        break;
                }
            }
        }
    }
}