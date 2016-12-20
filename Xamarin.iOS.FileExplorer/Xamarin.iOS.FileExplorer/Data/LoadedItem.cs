using Foundation;

namespace Xamarin.iOS.FileExplorer.Data
{
	public class LoadedItem<T>
	{
		public NSUrl Url { get; }

		public ItemType Type { get; }

		public NSFileAttributes Attributes { get; }
		public T Resource { get; }

		public LoadedItem(Item<T> item, NSFileAttributes attributes, T resource)
		{
			Url = item.Url;
			Type = item.Type;
			Attributes = attributes;
			Resource = resource;
		}
	}
}