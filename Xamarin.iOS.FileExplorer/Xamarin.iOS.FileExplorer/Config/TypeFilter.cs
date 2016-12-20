using System;

namespace Xamarin.iOS.FileExplorer.Config
{
	public sealed class TypeFilter : Filter
	{
		public TypeFilter(Type filterType)
		{
			FilterType = filterType;
		}

		public Type FilterType { get; }

		protected override bool MatchesItem(string lastPathComponent, Type itemType, DateTime modificationDate)
		{
			return itemType == FilterType;
		}
	}
}