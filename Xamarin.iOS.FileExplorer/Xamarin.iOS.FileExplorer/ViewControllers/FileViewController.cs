using System;
using CoreGraphics;
using UIKit;
using Xamarin.iOS.FileExplorer.CustomViews;
using Xamarin.iOS.FileExplorer.Extensions;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
    public class FileViewController : UIViewController
    {
        private FileViewModel viewModel;
        private UIScrollView scrollView;
        private UIStackView stackView;

        public FileViewController(FileViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        protected internal FileViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            ExtendedLayoutIncludesOpaqueBars = false;
            EdgesForExtendedLayout = UIRectEdge.None;

            var imageView = new ImageView();
            imageView.TranslatesAutoresizingMaskIntoConstraints = false;
            imageView.SetContentCompressionResistancePriority((float)UILayoutPriority.DefaultLow, UILayoutConstraintAxis.Vertical);
            imageView.SetContentCompressionResistancePriority((float)UILayoutPriority.DefaultLow, UILayoutConstraintAxis.Horizontal);

            var titleView = new TitleView();
            titleView.TranslatesAutoresizingMaskIntoConstraints = false;
            titleView.Title = viewModel.Title;
            titleView.SetContentCompressionResistancePriority((float)UILayoutPriority.Required, UILayoutConstraintAxis.Vertical);
            titleView.SetContentCompressionResistancePriority((float)UILayoutPriority.Required, UILayoutConstraintAxis.Horizontal);

            var attributesView = new AttributesView();
            attributesView.TranslatesAutoresizingMaskIntoConstraints = false;
            attributesView.NumberOfAttributes = viewModel.NumberOfAttributes;
            attributesView.SetContentCompressionResistancePriority((float)UILayoutPriority.Required, UILayoutConstraintAxis.Vertical);
            attributesView.SetContentCompressionResistancePriority((float)UILayoutPriority.Required, UILayoutConstraintAxis.Horizontal);

            for (int i = 0; i < attributesView.AttributesNamesColumn.Labels.Count; i++)
            {
                var attributeViewModel = viewModel.Atrribute(i);
                attributesView.AttributesNamesColumn.Labels[i].Text = attributeViewModel.AttributeName;
            }
            for (int i = 0; i < attributesView.AttributesValuesColumn.Labels.Count; i++)
            {
                var attributeViewModel = viewModel.Atrribute(i);
                attributesView.AttributesNamesColumn.Labels[i].Text = attributeViewModel.AttributeValue;
            }

            stackView.AddArrangedSubview(imageView);
            stackView.AddArrangedSubview(titleView);
            stackView.AddArrangedSubview(attributesView);
            stackView.Axis = UILayoutConstraintAxis.Vertical;
            stackView.Distribution = UIStackViewDistribution.Fill;
            stackView.TranslatesAutoresizingMaskIntoConstraints = false;

            View.AddSubview(scrollView);
            scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            scrollView.AlwaysBounceVertical = true;
            scrollView.AddSubview(stackView);

            stackView.TopAnchor.ConstraintEqualTo(scrollView.TopAnchor).Active = true;
            stackView.HeightAnchor.ConstraintEqualTo(View.HeightAnchor).Active = true;
            stackView.WidthAnchor.ConstraintEqualTo(View.WidthAnchor).Active = true;

            this.SetActiveNavigationItemTitle(viewModel.Title);

            View.SetNeedsLayout();
            View.LayoutIfNeeded();

            imageView.CustomImage = viewModel.ThumbnailImage(new CGSize(imageView.Bounds.Width, imageView.Bounds.Height));
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            scrollView.Frame = View.Bounds;
            scrollView.ContentSize = View.Bounds.Size;
            stackView.Frame = new CGRect(new CGPoint(0, 0), View.Bounds.Size);
            stackView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
        }
    }
}
