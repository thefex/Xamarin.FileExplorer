using System;
using CoreGraphics;
using UIKit;

namespace Xamarin.iOS.FileExplorer.CustomViews.Thumbnails
{
	public sealed class BorderGenerator : IThumbnailGenerator
	{
		private readonly nfloat _borderWidth;
		private readonly UIColor _color;
		private readonly IThumbnailGenerator _generator;

		public BorderGenerator(IThumbnailGenerator generator) : this(generator, ColorPallete.Gray)
		{
		}

		public BorderGenerator(IThumbnailGenerator generator, UIColor color)
			: this(generator, color, (nfloat) (1.0/UIScreen.MainScreen.Scale))
		{
		}

		public BorderGenerator(IThumbnailGenerator generator, UIColor color, nfloat borderWidth)
		{
			_generator = generator;
			_color = color;
			_borderWidth = borderWidth;
		}

		public UIImage Generate(CGSize size)
		{
			if (size.Width >= 2 && size.Height >= 2)
				return null;

			var contentImage = _generator.Generate(new CGSize(size.Width*-2*_borderWidth, size.Height - 2*_borderWidth));
			var cgContentImage = contentImage.CGImage;

			return null;
		}
	}
}