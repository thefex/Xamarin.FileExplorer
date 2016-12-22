using CoreGraphics;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews
{
    public class ImageView : UIView
    {
        private UIImageView customImageView;

        public ImageView()
        {
            Initialize();
        }

        public ImageView(CGRect frame) : base(frame)
        {
            Initialize();
        }

        private void Initialize()
        {
            customImageView = new UIImageView();
            customImageView.TranslatesAutoresizingMaskIntoConstraints = false;

            AddSubview(customImageView);
            customImageView.BackgroundColor = UIColor.White;
            customImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            customImageView.CenterXAnchor.ConstraintEqualTo(CenterXAnchor).Active = true;
            customImageView.CenterYAnchor.ConstraintEqualTo(CenterYAnchor).Active = true;
            customImageView.WidthAnchor.ConstraintEqualTo(WidthAnchor, -40).Active = true;
            customImageView.HeightAnchor.ConstraintEqualTo(HeightAnchor, -38).Active = true;
        }

        public UIImage CustomImage
        {
            get { return customImageView.Image; }
            set { customImageView.Image = value; }
        }
    }
}