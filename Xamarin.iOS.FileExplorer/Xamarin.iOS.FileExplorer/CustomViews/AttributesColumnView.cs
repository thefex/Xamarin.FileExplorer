using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using Xamarin.iOS.FileExplorer.Extensions;

namespace Xamarin.iOS.FileExplorer.CustomViews
{
    public class AttributesColumnView : UIView
    {
        public IList<UILabel> Labels = new List<UILabel>();
        private UIStackView stackView = new UIStackView();

        private void Initialize()
        {
            stackView.TranslatesAutoresizingMaskIntoConstraints = false;
            stackView.Axis = UILayoutConstraintAxis.Vertical;
            stackView.Distribution = UIStackViewDistribution.FillEqually;
            stackView.Spacing = 10f;
            AddSubview(stackView);
            stackView.Edges(this, UIEdgeInsets.Zero);
        }

        public AttributesColumnView(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public AttributesColumnView()
        {
            Initialize();
        }

        private int _numberOfAttributes;

        public int NumberOfAttributes
        {
            get { return _numberOfAttributes; }
            set
            {
                _numberOfAttributes = value;
                foreach (var view in Labels)
                {
                    stackView.RemoveArrangedSubview(view);
                    view.RemoveFromSuperview();
                }
                for (int i = 0; i < _numberOfAttributes; i++)
                {
                    var label = new UILabel();
                    Labels.Add(label);
                    stackView.AddArrangedSubview(label);
                }
            }
        }
    }
}