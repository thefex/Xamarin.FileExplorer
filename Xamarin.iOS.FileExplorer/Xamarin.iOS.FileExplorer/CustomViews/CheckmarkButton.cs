using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews
{
	public sealed class CheckmarkButton : UIButton
	{
		private nfloat _borderWidth = 1f;
		private readonly CAShapeLayer shapeLayer;

		private static readonly string KeyPathStrokeEnd = "strokeEnd";
		private static readonly string KeyPathBackgroundColor = "backgroundColor";


		public CheckmarkButton(NSCoder coder) : base(coder)
		{
		}

		public CheckmarkButton(NSObjectFlag t) : base(t)
		{
		}

		public CheckmarkButton(IntPtr handle) : base(handle)
		{
		}

		public CheckmarkButton(CGRect frame) : base(frame)
		{
			shapeLayer = new CAShapeLayer
			{
				ContentsScale = UIScreen.MainScreen.Scale,
				FillColor = UIColor.Clear.CGColor,
				StrokeColor = UIColor.White.CGColor,
				StrokeEnd = 0,
				BackgroundColor = UIColor.White.CGColor
			};
			shapeLayer.Actions = new NSDictionary(KeyPathStrokeEnd, new NSNull(), KeyPathBackgroundColor, new NSNull());

			Layer.AddSublayer(shapeLayer);
			Layer.MasksToBounds = true;
			BorderColor = ColorPallete.Gray;
			Selected = false;
			TouchUpInside += CheckmarkButton_TouchUpInside;

		}

		private void CheckmarkButton_TouchUpInside(object sender, EventArgs e)
		{
			var newIsSelected = !Selected;
			SetSelected(newIsSelected, true);
		}

		public nfloat BorderWidth
		{
			get { return _borderWidth; }
			set
			{
				_borderWidth = value;
				SetNeedsLayout();
			}
		}

		public override bool Selected
		{
			get { return base.Selected; }
			set
			{
				SetSelected(value, false);
			}
		}

		public override CGSize IntrinsicContentSize => new CGSize(22f, 22f);


		private void SetSelected(bool newValue, bool isAnimated)
		{
			if (Selected == newValue)
				return;

			Selected = newValue;
			CATransaction.Begin();
			CATransaction.AnimationDuration = isAnimated ? 0.2f : 0f;

			if (Selected)
			{
				ShapeAnimation(shapeLayer, KeyPathStrokeEnd, null, new NSNumber(1.0));
				ShapeAnimation(shapeLayer, KeyPathBackgroundColor, null, NSObject.FromObject(ColorPallete.Blue.CGColor));
				BorderColor = UIColor.White;
			}
			else
			{
				CATransaction.Begin();
				CATransaction.AnimationDuration = 0f;
				ShapeAnimation(shapeLayer, KeyPathStrokeEnd, null, new NSNumber(1));
				CATransaction.Commit();
				ShapeAnimation(shapeLayer, KeyPathBackgroundColor, null, FromObject(UIColor.White.CGColor));
				BorderColor = ColorPallete.Gray;
			}

			CATransaction.Commit();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			Layer.CornerRadius = Bounds.Width/2f;
			shapeLayer.Frame = Bounds.Inset(BorderWidth, BorderWidth);
		}

		public UIColor BorderColor
		{
			get { return BackgroundColor; }
			set { BackgroundColor = value; }
		}

		CAAnimation ShapeAnimation(CAShapeLayer layer, string keyPath, NSObject from, NSObject to, float duration = 0.2f)
		{
			if (from == to)
				return null;

			var animation = new CABasicAnimation {KeyPath = keyPath};
			animation.From = ValueForKeyPath(new NSString(keyPath));
			animation.To = to;
			animation.Duration = duration;
			layer.AddAnimation(animation, keyPath);
			layer.SetValueForKey(to, new NSString(keyPath));

			return animation;
		}
	}
}