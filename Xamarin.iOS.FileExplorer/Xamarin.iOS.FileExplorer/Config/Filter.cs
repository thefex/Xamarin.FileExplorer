using System;
using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.Config
{
	public abstract class Filter
	{
		
		public FilterType Type { get; set; }

		public bool MatchesItem<T>(Item<T> item)
		{
			return MatchesItem(null, typeof(Filter), DateTime.UtcNow);
		}

		protected abstract bool MatchesItem(string lastPathComponent, Type itemType, DateTime modificationDate);

	}
}