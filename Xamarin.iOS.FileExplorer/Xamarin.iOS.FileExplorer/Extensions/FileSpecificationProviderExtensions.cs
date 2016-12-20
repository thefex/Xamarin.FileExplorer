using System.Linq;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;
using Xamarin.iOS.FileExplorer.ViewControllers;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class FileSpecificationProviderExtensions
	{
		public static bool DoesDescirbeItem<T>(this IFileSpecificationProvider provider, Item<T> item)
			=> provider.Extensions.Any(x => x == "");
	}
}