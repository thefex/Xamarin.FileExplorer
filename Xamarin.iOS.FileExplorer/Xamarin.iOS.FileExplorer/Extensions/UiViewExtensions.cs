using UIKit;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class UiViewExtensions
	{
		public static NSLayoutConstraint PinToBottom(this UIView view, UIView ofView)
		{
			var constraint = view.BottomAnchor.ConstraintEqualTo(ofView.BottomAnchor);
			constraint.Active = true;
			view.LeadingAnchor.ConstraintEqualTo(ofView.LeadingAnchor).Active = true;
			view.TrailingAnchor.ConstraintEqualTo(ofView.TrailingAnchor).Active = true;
			view.HeightAnchor.ConstraintEqualTo(view.Bounds.Height).Active = true;
			return constraint;
		}

		public static void Edges(this UIView view, UIView equalToView, UIEdgeInsets edgeInsets)
		{
			view.LeadingAnchor.ConstraintEqualTo(equalToView.LeadingAnchor, edgeInsets.Left).Active = true;
			view.TrailingAnchor.ConstraintEqualTo(equalToView.TrailingAnchor, edgeInsets.Right).Active = true;
			view.TopAnchor.ConstraintEqualTo(equalToView.TopAnchor, edgeInsets.Top).Active = true;
			view.BottomAnchor.ConstraintEqualTo(equalToView.BottomAnchor, edgeInsets.Bottom).Active = true;
		}
	}
}