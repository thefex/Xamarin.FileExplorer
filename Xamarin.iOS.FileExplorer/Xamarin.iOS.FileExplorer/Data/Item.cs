using Foundation;
using System;
using System.IO;

namespace Xamarin.iOS.FileExplorer.Data
{
	public class Item<T>
	{
		private readonly NSFileAttributes _attributes;
		private readonly Func<NSFileAttributes, NSData, NSUrl, T> _parse;

		public Item(NSUrl url, NSFileAttributes attributes, ItemType type, Func<NSFileAttributes, NSData, NSUrl, T> parse)
		{
			Url = url;
			_attributes = attributes;
			Type = type;
			_parse = parse;

		}

		public string Name => Url.LastPathComponent;

		public string Extension => new NSString(Url.LastPathComponent).PathExtension;

		public NSDate ModificationDate => _attributes.ModificationDate;

		public NSUrl Url { get; }

		public ItemType Type { get; }

		public T Parse(NSFileAttributes fileAttributes, NSData data, NSUrl url) => _parse(fileAttributes, data, url);

		public override bool Equals(object obj)
		{
			var other = obj as Item<T>;
			if (other == null)
				return false;

			return other.Url == Url;
		}

		public override int GetHashCode() => Url.GetHashCode();
	}
}