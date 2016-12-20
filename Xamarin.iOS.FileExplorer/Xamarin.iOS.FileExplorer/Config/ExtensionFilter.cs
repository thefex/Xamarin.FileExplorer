using System;

namespace Xamarin.iOS.FileExplorer.Config
{
	public sealed class ExtensionFilter : Filter
	{
		protected override bool MatchesItem(string lastPathComponent, Type itemType, DateTime modificationDate)
		{
			return "extension".Equals(lastPathComponent, StringComparison.OrdinalIgnoreCase);
		}
	}
}