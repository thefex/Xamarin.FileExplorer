using System;

namespace Xamarin.iOS.FileExplorer.Config
{
	public sealed class ModificationDatePastFilter : Filter
	{
		public ModificationDatePastFilter(DateTime date)
		{
			Date = date;
		}

		public DateTime Date { get; }

		protected override bool MatchesItem(string lastPathComponent, Type itemType, DateTime modificationDate)
		{
			return (Date - modificationDate).TotalSeconds > 0;
		}
	}
}