using System;
using System.Collections.Generic;
using AVFoundation;
using AVKit;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public class VideoSpecificationProvider : IFileSpecificationProvider
	{
		public IEnumerable<string> Extensions { get; } = new List<string>() {"mp4", "avi"};
		public UIImage GetThumbnail(NSUrl atUri, CGSize withSize)
		{
			throw new NotImplementedException();
		}

		public UIViewController GetViewControllerForItem(NSUrl atUri, NSData data, NSFileAttributes fileAttribute)
		{
			var player = new AVPlayer(atUri);
			return new AVPlayerViewController()
			{
				Player = player
			};
		}
	}
}