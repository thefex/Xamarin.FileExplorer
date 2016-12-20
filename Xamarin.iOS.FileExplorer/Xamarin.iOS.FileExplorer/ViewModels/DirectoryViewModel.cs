using Foundation;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.ViewModels
{
	public class DirectoryViewModel
	{
		public bool FinishButtonHidden { get; }

		NSUrl url;
		LoadedItem<object> item;
		FileSpecifications fileSpecifications;
		Configuration configuration;

		public DirectoryViewModel(NSUrl url, LoadedItem<object> loadedDirectoryItem, FileSpecifications fileSpecifications, Configuration configuration, bool finishButtonHidden)
		{
			this.url = url;
			this.item = loadedDirectoryItem;
			this.fileSpecifications = fileSpecifications;
			this.configuration = configuration;
			this.FinishButtonHidden = finishButtonHidden;
		}

		public string FinishButtonTitle
		{
			get
			{
				if (configuration.ActionsConfiguration.CanChooseFiles || configuration.ActionsConfiguration.CanChooseDirectories)
					return "Cancel";
				else
					return "Done";
			}
		}

		public DirectoryContentViewModel BuildDirectoryContentViewModel()
			=> new DirectoryContentViewModel(item, fileSpecifications, configuration);
	}
}