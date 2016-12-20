using System;

namespace Xamarin.iOS.FileExplorer.Config
{
	public sealed class LastPathComponentFilter : Filter
	{
		public LastPathComponentFilter(string lastPathComponent)
		{
			LastPathComponent = lastPathComponent;
		}

		public string LastPathComponent { get; }

		protected override bool MatchesItem(string lastPathComponent, Type itemType, DateTime modificationDate)
		{
			return LastPathComponent == lastPathComponent;
		}
	}
}