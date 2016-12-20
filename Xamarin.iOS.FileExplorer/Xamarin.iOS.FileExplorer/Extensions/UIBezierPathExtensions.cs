using System;
using CoreGraphics;
using UIKit;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class UIBezierPathExtensions
	{
		public static UIBezierPath MakeCheckmarkPath(CGRect withFrame)
		{
			var path = new UIBezierPath();
			nfloat minX = withFrame.GetMinX();
			nfloat minY = withFrame.GetMinY();
			nfloat width = withFrame.Width;
			nfloat height = withFrame.Height;

			path.MoveTo(new CGPoint(minX + 0.07692*width, minY + 0.57143*height));
			path.AddLineTo(new CGPoint(minX + 0.30769 * width, minY + 0.85714*height));
			path.AddLineTo(new CGPoint(minX + 0.92308*width, minY + 0.09524*height));
			path.LineWidth = 2;
			return path;
		}
	}

	
}