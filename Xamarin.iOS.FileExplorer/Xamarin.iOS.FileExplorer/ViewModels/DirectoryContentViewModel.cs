using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
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

        public DirectoryContentViewModel(LoadedItem<object> loadedDirectoryItem, FileSpecifications fileSpecifications,
            Configuration configuration)
            : this(loadedDirectoryItem, fileSpecifications, configuration, new LocalStorageFileService<object>())
        {

        }

        public DirectoryContentViewModel(LoadedItem<object> loadedDirectoryItem, FileSpecifications fileSpecifications,
            Configuration configuration, IFileService<object> fileService)
        {
            url = loadedDirectoryItem.Url;
            _fileSpecifications = fileSpecifications;
            this.configuration = configuration;
            _fileService = fileService;
        }

        public SortMode SortMode { get; set; }

        public bool IsDeleteActionEnabled { get; set; }
        public bool IsEditActionHidden { get; set; }
        public string IsEditActionTitle { get; set; }
        public bool IsEditActionEnabled { get; set; }
        public string Title { get; set; }
        public bool IsUserInteractionEnabled { get; set; }
        public bool IsEditing { get; set; }
        public bool IsSelectActionHidden { get; set; }
        public string SelectActionTitle { get; set; }
        public bool IsSelectActionEnabled { get; set; }
        public string DeleteActionTitle { get; set; }
        public bool IsDeleteActionHidden { get; set; }

        public nfloat NumberOfItems(int section)
        {
            return _itemsToDisplay.Count;
        }
    }
}