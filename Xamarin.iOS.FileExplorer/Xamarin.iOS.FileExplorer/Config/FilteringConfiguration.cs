using System.Collections.Generic;
using System.Linq;

namespace Xamarin.iOS.FileExplorer.Config
{
	public class FilteringConfiguration
	{
		public IEnumerable<string> FileFilters { get; set; } = Enumerable.Empty<string>();

		public IEnumerable<string> IgnoredFileFilters { get; set; } = Enumerable.Empty<string>();
	}
}