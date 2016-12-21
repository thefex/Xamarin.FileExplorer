using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews
{
    public sealed class CollectionViewFooter : UICollectionReusableView
	{
		nfloat leftInset = 0;
		readonly IList<SeparatorView> separators = new List<SeparatorView>();

		public CollectionViewFooter()
		{
			Initialize();
		}

		protected internal CollectionViewFooter(IntPtr handle) : base(handle)
		{
		}

		public CollectionViewFooter(CGRect frame) : base(frame)
		{
			Initialize();
		}

		private void Initialize()
		{
			for (int i = 0; i < 15; ++i)
				separators.Add(new SeparatorView());

			foreach (var separator in separators)
				Add(separator);
			TintColor = ColorPallete.Gray;
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			for (int index = 0; index < separators.Count; index++)
			{
				var separator = separators[index];
				var size = new CGSize(Bounds.Width - leftInset, 1);
				var newFrameSize = separator.SizeThatFits(size);
				var newFrameOrigin = new CGPoint(leftInset, (index + 1)*64 - size.Height);

				separator.Frame = new CGRect(newFrameOrigin, newFrameSize);
			}
		}

		public override UIColor TintColor
		{
			get
			{
				return base.TintColor;
			}

			set
			{
				base.TintColor = value;
				AdjustAfterTintColorChange();
			}
		}

		public override void TintColorDidChange()
		{
			AdjustAfterTintColorChange();
		}

		private void AdjustAfterTintColorChange()
		{
			foreach (var separator in separators)
				separator.BackgroundColor = TintColor;
		}
	}
}