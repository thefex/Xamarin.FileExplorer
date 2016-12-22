using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.CustomViews;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.ViewModels
{
	public class ItemViewModel
	{
		private static readonly NSDateFormatter DateFormatter = new NSDateFormatter();

		public ItemViewModel(Item<object> item, IFileSpecificationProvider provider)
		{
			Item = item;
			Provider = provider;
			Title = item.Name;
			Subtitle = ModificiationDateToString();
			Accessory = item.Type == ItemType.Directory
				? ItemCell.AccessoryType.DisclousoureIndicator
				: ItemCell.AccessoryType.DetailButton;
		}

		public Item<object> Item { get; }

		public IFileSpecificationProvider Provider { get; }

		public string Title { get; }

		public string Subtitle { get; }

		public ItemCell.AccessoryType Accessory { get; }

		private string ModificiationDateToString()
		{
			var date = Item.ModificationDate;
			DateFormatter.TimeStyle = NSDateFormatterStyle.None;
			DateFormatter.DateStyle = NSDateFormatterStyle.Medium;

			return DateFormatter.ToString(date);
		}

		public UIImage GetThumbnailImage(CGSize size)
		{
			switch (Item.Type)
			{
				case ItemType.Directory:
					return ImageAssets.GenericDirectoryIcon;
				case ItemType.File:
					return Provider.GetThumbnail(Item.Url, size) ?? ImageAssets.GenericDocumentIcon;
			}

			throw new InvalidOperationException();
		}


	}
}