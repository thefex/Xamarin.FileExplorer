using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.ViewModels
{
    public class DirectoryContentViewModel
    {
        private IList<Item<object>> _selectedItems = new List<Item<object>>();
        private readonly IList<Item<object>> _allItems = new List<Item<object>>();
        private IEnumerable<Item<object>> _itemsToDisplay = new List<Item<object>>();
        private readonly NSUrl url;
        private readonly Configuration configuration = new Configuration();
        private readonly FileSpecifications _fileSpecifications;
        private readonly IFileService<object> _fileService;
	    private SortMode sortMode;
	    private bool isEditing;

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



	    public SortMode SortMode
	    {
		    get { return sortMode; }
		    set
		    {
			    sortMode = value;
			    _itemsToDisplay = DirectoryContentViewModel.GetItemsWithAppliedFilterAndSortCriterias(SearchQuery, SortMode, _allItems);
		    }
	    }

	    public bool IsUserInteractionEnabled => _fileService.IsDeletionInProgress;
	    public string Title => url.LastPathComponent;

	    public bool IsEditing
	    {
		    get { return isEditing; }
		    set
		    {
				if (isEditing == value)
					return;

				isEditing = value;
			    _selectedItems = new List<Item<object>>();
			    Delegate?.Changed(this);
		    }
	    }
	    public string SearchQuery { get; set; }

	    public bool IsDeleteActionEnabled { get; set; }
	    public bool IsEditActionHidden { get; set; }
	    public string IsEditActionTitle { get; set; }
	    public bool IsEditActionEnabled { get; set; }
	    public bool IsSelectActionHidden { get; set; }
        public string SelectActionTitle { get; set; }
        public bool IsSelectActionEnabled { get; set; }
        public string DeleteActionTitle { get; set; }
        public bool IsDeleteActionHidden { get; set; }

        public nfloat NumberOfItems(int section)
        {
            return _itemsToDisplay.Count;
        }

	    private static IEnumerable<Item<object>> GetItemsWithAppliedFilterAndSortCriterias(string searchQuery,
		    SortMode sortMode, IEnumerable<Item<object>> items)
	    {
		    searchQuery = searchQuery.Trim();
		    var filteredItems =
			    items.Where(x => x.Url.LastPathComponent.Contains(searchQuery) || string.IsNullOrWhiteSpace(searchQuery));

		    if (sortMode == SortMode.Date)
			    filteredItems = filteredItems.OrderBy(x => x.ModificationDate);
			else if (sortMode == SortMode.Name)
				filteredItems = filteredItems.OrderBy(x => x.Name);

		    return filteredItems.ToList();
	    }

		public IDirectoryContentViewModelDelegate Delegate { get; set; }
    }

	public interface IDirectoryContentViewModelDelegate
	{
		void ListChanged(DirectoryContentViewModel viewModel);

		void Changed(DirectoryContentViewModel viewModel);

		void ItemSelected(Item<object> selectedItem);
	}
}