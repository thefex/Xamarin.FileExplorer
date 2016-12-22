using CoreGraphics;
using UIKit;
using Xamarin.iOS.FileExplorer.Extensions;

namespace Xamarin.iOS.FileExplorer.CustomViews
{
    public class AttributesView : UIView
    {
        private UIStackView stackView = new UIStackView();
        public AttributesColumnView AttributesNamesColumn = new AttributesColumnView();
        public AttributesColumnView AttributesValuesColumn = new AttributesColumnView();

        public AttributesView()
        {
            Initialize();
        }

        public AttributesView(CGRect frame) : base(frame)
        {
            Initialize();
        }

        private void Initialize()
        {
            stackView.AddArrangedSubview(AttributesNamesColumn);
            AttributesNamesColumn.TranslatesAutoresizingMaskIntoConstraints = false;

            stackView.AddArrangedSubview(AttributesValuesColumn);
            AttributesValuesColumn.TranslatesAutoresizingMaskIntoConstraints = false;

            stackView.TranslatesAutoresizingMaskIntoConstraints = false;
            stackView.Axis = UILayoutConstraintAxis.Horizontal;
            stackView.LayoutMargins = new UIEdgeInsets(16, 0, 176, 0);

            stackView.LayoutMarginsRelativeArrangement = true;
            stackView.Distribution = UIStackViewDistribution.FillProportionally;

            stackView.Spacing = 10;
            AddSubview(stackView);
            stackView.Edges(this, UIEdgeInsets.Zero);
        }

        private int _numberOfAttributes;
        public int NumberOfAttributes
        {
            get { return _numberOfAttributes; }
            set
            {
                _numberOfAttributes = value;
                AttributesNamesColumn.NumberOfAttributes = value;
                foreach (var label in AttributesNamesColumn.Labels)
                {
                    label.TranslatesAutoresizingMaskIntoConstraints = false;
                    label.TextAlignment = UITextAlignment.Right;
                    label.Font = UIFont.SystemFontOfSize(15);
                    label.TextColor = ColorPallete.Gray;
                }
                AttributesValuesColumn.NumberOfAttributes = value;
                foreach (var label in AttributesValuesColumn.Labels)
                {
                    label.TranslatesAutoresizingMaskIntoConstraints = false;
                    label.TextAlignment = UITextAlignment.Left;
                    label.Font = UIFont.SystemFontOfSize(15);
                    label.TextColor = UIColor.Black;
                }
            }
        }
    }
}