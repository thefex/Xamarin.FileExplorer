using CoreGraphics;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews
{
    public class TitleView : UIView
    {
        private UILabel titleLabel;

        public TitleView()
        {
            Initialize();
        }

        public TitleView(CGRect frame) : base(frame)
        {
            Initialize();
        }

        private void Initialize()
        {
            titleLabel = new UILabel();
            titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            titleLabel.TextAlignment = UITextAlignment.Center;
            titleLabel.Font = UIFont.SystemFontOfSize(23f);

            AddSubview(titleLabel);
            titleLabel.CenterXAnchor.ConstraintEqualTo(CenterXAnchor).Active = true;
            titleLabel.CenterYAnchor.ConstraintEqualTo(CenterYAnchor, -1).Active = true;
            titleLabel.WidthAnchor.ConstraintEqualTo(WidthAnchor, -20).Active = true;
            titleLabel.SetContentHuggingPriority((float) UILayoutPriority.Required, UILayoutConstraintAxis.Vertical);
            titleLabel.Lines = 1;
            titleLabel.LineBreakMode = UILineBreakMode.TailTruncation;

            var topSeparator = new SeparatorView();
            topSeparator.TranslatesAutoresizingMaskIntoConstraints = false;
            topSeparator.BackgroundColor = ColorPallete.Gray;
            AddSubview(topSeparator);
            topSeparator.LeadingAnchor.ConstraintEqualTo(LeadingAnchor).Active = true;
            topSeparator.TrailingAnchor.ConstraintEqualTo(TrailingAnchor).Active = true;
            topSeparator.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;

            var bottomSeparator = new SeparatorView();
            bottomSeparator.TranslatesAutoresizingMaskIntoConstraints = false;
            bottomSeparator.BackgroundColor = ColorPallete.Gray;
            AddSubview(bottomSeparator);

            bottomSeparator.LeadingAnchor.ConstraintEqualTo(LeadingAnchor).Active = true;
            bottomSeparator.TrailingAnchor.ConstraintEqualTo(TrailingAnchor).Active = true;
            bottomSeparator.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
        }


        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }

        public override CGSize IntrinsicContentSize => new CGSize(UIView.NoIntrinsicMetric, 42);
    }
}