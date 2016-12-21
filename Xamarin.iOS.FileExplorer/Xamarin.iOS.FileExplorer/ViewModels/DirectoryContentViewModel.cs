using System.Collections.Generic;
using Foundation;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.ViewModels
{
	public class DirectoryContentViewModel
	{
		private readonly IList<Item<object>> _selectedItems = new List<Item<object>>();
		private readonly IList<Item<object>> _allItems = new List<Item<object>>();
		private readonly IList<Item<object>> _itemsToDisplay = new List<Item<object>>();
		private readonly NSUrl url;
		private readonly Configuration configuration = new Configuration();
		private readonly FileSpecifications _fileSpecifications;
		private readonly IFileService<object> _fileService;

		public DirectoryContentViewModel(LoadedItem<object> loadedDirectoryItem, FileSpecifications fileSpecifications, Configuration configuration) 
			: this(loadedDirectoryItem, fileSpecifications, configuration, new LocalStorageFileService<object>())
		{
			
		}

		public DirectoryContentViewModel(LoadedItem<object> loadedDirectoryItem, FileSpecifications fileSpecifications, Configuration configuration, IFileService<object> fileService)
		{
			url = loadedDirectoryItem.Url;
			_fileSpecifications = fileSpecifications;
			this.configuration = configuration;
			_fileService = fileService;
			SortMode = SortMode.Name;

			var filteringConfiguration = configuration.FilteringConfiguration;
			//_allItems = loadedDirectoryItem.Resource
		}

		public enum ViewModelError
		{
			FailedItemsRemoval
		};

		private SortMode sortMode;
		public SortMode SortMode
		{
			get { return sortMode; }
			set
			{
				
			}
		}
		 
	}
}