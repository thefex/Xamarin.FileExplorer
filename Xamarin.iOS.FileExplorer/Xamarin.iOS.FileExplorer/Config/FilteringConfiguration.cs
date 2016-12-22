using System.Collections.Generic;
using System.Linq;

namespace Xamarin.iOS.FileExplorer.Config
{
	public class FilteringConfiguration
	{
		public IEnumerable<Filter> FileFilters { get; set; } = Enumerable.Empty<Filter>();

		public IEnumerable<Filter> IgnoredFileFilters { get; set; } = Enumerable.Empty<Filter>();
	}
}