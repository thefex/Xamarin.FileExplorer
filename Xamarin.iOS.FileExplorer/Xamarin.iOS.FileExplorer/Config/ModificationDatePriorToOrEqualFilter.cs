using System;

namespace Xamarin.iOS.FileExplorer.Config
{
	public sealed class ModificationDatePriorToOrEqualFilter : Filter
	{
		public ModificationDatePriorToOrEqualFilter(DateTime date)
		{
			Date = date;
		}

		public DateTime Date { get; }

		protected override bool MatchesItem(string lastPathComponent, Type itemType, DateTime modificationDate)
		{
			return (Date - modificationDate).TotalSeconds <= 0;
		}
	}
}