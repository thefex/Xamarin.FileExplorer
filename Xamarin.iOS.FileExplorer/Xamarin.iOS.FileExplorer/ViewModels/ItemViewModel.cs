using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.ViewModels
{
	public class ItemViewModel<T>
	{
		private static readonly NSDateFormatter DateFormatter = new NSDateFormatter();

		public ItemViewModel(Item<T> item, IFileSpecificationProvider provider)
		{
			Item = item;
			Provider = provider;
			
		}

		public Item<T> Item { get; }

		public IFileSpecificationProvider Provider { get; }

		public string Title { get; }

		public string Subtitle { get; }

		public UIImage GetThumbnailImage(CGSize size)
		{
			return null;
		}


	}
}