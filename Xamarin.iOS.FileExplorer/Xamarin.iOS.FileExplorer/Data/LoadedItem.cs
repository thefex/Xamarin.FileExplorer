using Foundation;

namespace Xamarin.iOS.FileExplorer.Data
{
	public class LoadedItem<T>
	{
        public string Name { get; }

		public NSUrl Url { get; }

		public ItemType Type { get; }

		public NSFileAttributes Attributes { get; }
		public T Resource { get; }

		public LoadedItem(Item<T> item, NSFileAttributes attributes, T resource)
		{
		    Name = item.Name;
			Url = item.Url;
			Type = item.Type;
			Attributes = attributes;
			Resource = resource;
		}
	}
}