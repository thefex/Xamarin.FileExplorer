using System;
using UIKit;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public class UnknownFileTypeViewController : UIViewController
	{
		private readonly string _fileName;
		private UIImageView imageView;

		public UnknownFileTypeViewController(string fileName)
		{
			_fileName = fileName;
		}

		public UnknownFileTypeViewController(IntPtr ptr) : base(ptr)
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;
			SetupImageView();
			SetupTextLabel();
		}

		private void SetupImageView()
		{
			imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			imageView.TranslatesAutoresizingMaskIntoConstraints = false;
			imageView.Image = UIImage.FromBundle("genericDocumentIcon");
			Add(imageView);
			imageView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 70.0f).Active = true;
			imageView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -70.0f).Active = true;
			imageView.HeightAnchor.ConstraintEqualTo(imageView.WidthAnchor, 1).Active = true;
			imageView.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
			imageView.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor, -50f).Active = true;
		}

		private void SetupTextLabel()
		{
			var label = new UILabel()
			{
				Font = UIFont.SystemFontOfSize(23f),
				TranslatesAutoresizingMaskIntoConstraints = false,
				Text = _fileName,
				TextAlignment = UITextAlignment.Center,
				LineBreakMode = UILineBreakMode.TailTruncation,
			};
			Add(label);
			label.TopAnchor.ConstraintEqualTo(imageView.BottomAnchor, 20f).Active = true;
			label.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 20f).Active = true;
			label.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -20f).Active = true;
		}
	}
}