using Foundation;
using System;
using System.IO;
using System.Linq;
using Xamarin.iOS.FileExplorer.Extensions;

namespace Xamarin.iOS.FileExplorer.Data
{
	public class Item<T>
	{
		private readonly NSFileAttributes _attributes;
		private readonly Func<NSFileAttributes, NSData, NSUrl[], T> _parse;

		public Item(NSUrl url, NSFileAttributes attributes, ItemType type, Func<NSFileAttributes, NSData, NSUrl[], T> parse)
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

		public T Parse(NSFileAttributes fileAttributes, NSData data, NSUrl[] url) => _parse(fileAttributes, data, url);

		public override bool Equals(object obj)
		{
			var other = obj as Item<T>;
			if (other == null)
				return false;

			return other.Url == Url;
		}

		public override int GetHashCode() => Url.GetHashCode();

		public static Item<object> FromDirectory(NSUrl directoryUrl, NSFileAttributes fileAttributes = null)
		{
			return new Item<object>(directoryUrl, fileAttributes, ItemType.Directory, (attributes, data, urls) =>
			{
				return urls.Select(x =>
				{
					NSObject resourceObject = null;
					x.TryGetResource(NSUrl.IsDirectoryKey, out resourceObject);

					bool isDirectoryObject = false;

					if (resourceObject != null)
					{
						var convertedNsObject = resourceObject.ToObject(typeof(bool));

						if (convertedNsObject != null)
							isDirectoryObject = (bool)convertedNsObject;
					}

					return At(x, attributes, isDirectoryObject);
				}).ToArray();
			});
		}

		public static Item<object> FromFile(NSUrl url, NSFileAttributes attributes, bool isDirectory)
		{
			return new Item<object>(url, attributes, ItemType.File, (fileAttributes, data, urls) => data);
		}

		public static Item<object> At(NSUrl url, NSFileAttributes attributes = null, bool isDirectory = false)
		{
			return isDirectory ? FromDirectory(url, attributes) : FromFile(url, attributes, isDirectory);
		}
	}
}