using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public interface IFileService
	{
		void Load(Item<object> item);
	}
}