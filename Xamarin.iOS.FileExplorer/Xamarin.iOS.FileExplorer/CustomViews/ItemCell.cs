using System;
using CoreGraphics;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews
{
	public sealed class ItemCell : UICollectionViewCell, IEditable, IUIGestureRecognizerDelegate
	{
		public enum AccessoryType
		{
			DetailButton,
			DisclousoureIndicator
		}

		private UIImageView accessoryImageView;

		private UITapGestureRecognizer accessoryImageViewTapRecognizer;
		private CheckmarkButton checkmarkButton;
		private UIView containerView;

		private NSLayoutConstraint containerViewLeadingConstraint;
		private NSLayoutConstraint containerViewTrailingConstraint;
		private AccessoryType customAccessoryType = AccessoryType.DetailButton;

		private UIImageView iconImageView;

		private bool isEditing;
		private SeparatorView separatorView;
		private UILabel subtitleTextLabel;
		private UILabel textLabel;

		protected internal ItemCell(IntPtr handle) : base(handle)
		{
			Initialize(Frame);
		}

		public ItemCell(CGRect frame) : base(frame)
		{
			Initialize(frame);
		}

		public string Title
		{
			get { return textLabel.Text; }
			set { textLabel.Text = value; }
		}

		public string Subtitle
		{
			get { return subtitleTextLabel.Text; }
			set { subtitleTextLabel.Text = value; }
		}

		public UIImage IconImage
		{
			get { return iconImageView.Image; }
			set { iconImageView.Image = value; }
		}

		public override bool Selected
		{
			get { return base.Selected; }

			set
			{
				base.Selected = value;
				checkmarkButton.Selected = value;
				SetNeedsLayout();
			}
		}

		public bool IsEditing
		{
			get { return isEditing; }
			set
			{
				isEditing = value;
				containerViewLeadingConstraint.Constant = isEditing ? 38 : 0;
				containerViewTrailingConstraint.Constant = isEditing ? 38 : 0;
				SetNeedsLayout();
			}
		}


		public AccessoryType Accessory
		{
			get { return customAccessoryType; }
			set
			{
				customAccessoryType = value;
				switch (value)
				{
					case AccessoryType.DetailButton:
						accessoryImageView.Image = UIImage.FromBundle("DetailButtonImage");
						accessoryImageViewTapRecognizer.Enabled = true;
						break;
					case AccessoryType.DisclousoureIndicator:
						accessoryImageView.Image = UIImage.FromBundle("DisclosureButtonImage");
						accessoryImageViewTapRecognizer.Enabled = false;
						break;
				}
			}
		}

		public CGSize MaximumIconSize
		{
			get
			{
				var max = Math.Max(Math.Max(iconImageView.Frame.Width, iconImageView.Frame.Height), LayoutConstants.IconWidth);
				return new CGSize(max, max);
			}
		}

		public Action TapAction { get; set; } = () => { };

		public void SetEditing(bool editing, bool animated)
		{
			if (animated)
			{
				Animate(0.2f, () =>
				{
					IsEditing = editing;
					LayoutIfNeeded();
				});
			}
			else
				IsEditing = editing;
		}

		private void Initialize(CGRect frame)
		{
			containerView = new UIView
			{
				BackgroundColor = UIColor.White
			};

			separatorView = new SeparatorView
			{
				BackgroundColor = ColorPallete.Gray
			};
			containerView.Add(separatorView);

			iconImageView = new UIImageView
			{
				ContentMode = UIViewContentMode.ScaleAspectFit
			};
			containerView.Add(iconImageView);

			textLabel = new UILabel
			{
				Lines = 1,
				LineBreakMode = UILineBreakMode.MiddleTruncation,
				Font = UIFont.SystemFontOfSize(17)
			};
			containerView.Add(textLabel);

			subtitleTextLabel = new UILabel
			{
				Lines = 1,
				LineBreakMode = UILineBreakMode.MiddleTruncation,
				Font = UIFont.SystemFontOfSize(12),
				TextColor = UIColor.Gray
			};
			containerView.Add(subtitleTextLabel);

			accessoryImageView = new UIImageView
			{
				ContentMode = UIViewContentMode.Center
			};
			containerView.Add(accessoryImageView);

			checkmarkButton = new CheckmarkButton(new CGRect());
			BackgroundColor = UIColor.White;

			accessoryImageViewTapRecognizer = new UITapGestureRecognizer(HandleAccessoryImageTap);
			accessoryImageViewTapRecognizer.Delegate = this;
			accessoryImageView.AddGestureRecognizer(accessoryImageViewTapRecognizer);
			accessoryImageView.UserInteractionEnabled = true;

			Add(checkmarkButton);
			Add(containerView);

			containerView.TranslatesAutoresizingMaskIntoConstraints = false;
			separatorView.TranslatesAutoresizingMaskIntoConstraints = false;
			textLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			subtitleTextLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			iconImageView.TranslatesAutoresizingMaskIntoConstraints = false;
			checkmarkButton.TranslatesAutoresizingMaskIntoConstraints = false;
			accessoryImageView.TranslatesAutoresizingMaskIntoConstraints = false;

			SetupContainerViewConstraint();
			SetupSeparatorViewConstraints();
			SetupIconImageViewConstraints();
			SetupAccessoryImageViewConstraints();
			SetupTitleLabelConstraints();
			SetupSubtitleLabelConstraints();
			SetupCheckmarkButtonConstraints();
		}

		private void SetupContainerViewConstraint()
		{
			containerViewLeadingConstraint = containerView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor);
			containerViewLeadingConstraint.Active = true;
			containerViewTrailingConstraint = containerView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor);
			containerViewTrailingConstraint.Active = true;
			containerView.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
			containerView.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
		}

		private void SetupSeparatorViewConstraints()
		{
			separatorView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor, LayoutConstants.SeparatorLeftInset).Active = true;
			separatorView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor).Active = true;
			separatorView.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor).Active = true;
		}

		private void SetupIconImageViewConstraints()
		{
			iconImageView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 24).Active = true;
			iconImageView.WidthAnchor.ConstraintEqualTo(LayoutConstants.IconWidth).Active = true;
			iconImageView.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, 10).Active = true;
			iconImageView.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor, -10).Active = true;
		}

		private void SetupAccessoryImageViewConstraints()
		{
			accessoryImageView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -15).Active = true;
			accessoryImageView.CenterYAnchor.ConstraintEqualTo(containerView.CenterYAnchor).Active = true;
			accessoryImageView.SetContentCompressionResistancePriority(1000, UILayoutConstraintAxis.Horizontal);
			accessoryImageView.SetContentCompressionResistancePriority(750, UILayoutConstraintAxis.Vertical);
			accessoryImageView.SetContentHuggingPriority(750, UILayoutConstraintAxis.Horizontal);
		}

		private void SetupTitleLabelConstraints()
		{
			textLabel.LeadingAnchor.ConstraintEqualTo(iconImageView.TrailingAnchor, 12).Active = true;
			textLabel.TrailingAnchor.ConstraintEqualTo(accessoryImageView.LeadingAnchor, -10).Active = true;
			textLabel.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, 12).Active = true;
			textLabel.SetContentCompressionResistancePriority(750, UILayoutConstraintAxis.Horizontal);
			textLabel.SetContentHuggingPriority(750, UILayoutConstraintAxis.Vertical);
		}

		private void SetupSubtitleLabelConstraints()
		{
			subtitleTextLabel.LeadingAnchor.ConstraintEqualTo(textLabel.LeadingAnchor).Active = true;
			subtitleTextLabel.TrailingAnchor.ConstraintEqualTo(textLabel.TrailingAnchor).Active = true;
			subtitleTextLabel.TopAnchor.ConstraintEqualTo(textLabel.BottomAnchor, 3).Active = true;
			subtitleTextLabel.SetContentCompressionResistancePriority(750, UILayoutConstraintAxis.Horizontal);
			subtitleTextLabel.SetContentHuggingPriority(750, UILayoutConstraintAxis.Vertical);
		}

		private void SetupCheckmarkButtonConstraints()
		{
			checkmarkButton.TrailingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, -4).Active = true;
			checkmarkButton.CenterYAnchor.ConstraintEqualTo(CenterYAnchor, 1).Active = true;
		}

		public override bool GestureRecognizerShouldBegin(UIGestureRecognizer gestureRecognizer)
		{
			return true;
		}

		private void HandleAccessoryImageTap()
		{
		}
	}
}