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
        private IList<Item<object>> _itemsToDisplay = new List<Item<object>>();
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
			    _itemsToDisplay = DirectoryContentViewModel.GetItemsWithAppliedFilterAndSortCriterias(SearchQuery, SortMode, _allItems).ToList();
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

	    private string searchQuery;
	    public string SearchQuery
	    {
		    get { return searchQuery; }
		    set
		    {
			    _itemsToDisplay = GetItemsWithAppliedFilterAndSortCriterias(searchQuery, SortMode, _allItems).ToList();
			    Delegate?.ListChanged(this);
		    }
	    }

	    public bool IsEditActionHidden
		    =>
			    !configuration.ActionsConfiguration.CanChooseFiles && !configuration.ActionsConfiguration.CanChooseDirectories &&
			    !configuration.ActionsConfiguration.CanRemoveFiles && !configuration.ActionsConfiguration.CanRemoveDirectories;

	    public string IsEditActionTitle => IsEditing ? "Cancel" : "Select";

	    public bool IsEditActionEnabled
		    => !IsEditActionHidden && !_fileService.IsDeletionInProgress;

	    public bool IsSelectActionHidden
		    => !configuration.ActionsConfiguration.CanChooseFiles && !configuration.ActionsConfiguration.CanChooseDirectories;

	    public string SelectActionTitle => "Choose";

	    public bool IsSelectionEnabled => IsEditing;

	    public bool IsSelectActionEnabled
	    {
		    get
		    {
				if (_selectedItems.Count == 0 || IsSelectActionHidden)
					return false;

				if (!configuration.ActionsConfiguration.CanChooseDirectories && _selectedItems.Any(x => x.Type == ItemType.Directory))
					return false;

				if (!configuration.ActionsConfiguration.CanChooseFiles && _selectedItems.Any(x => x.Type == ItemType.File))
					return false;

			    bool numberOfSelectedItemsAllowed = configuration.ActionsConfiguration.AllowsMultipleSelection
				    ? _selectedItems.Count > 0
				    : _selectedItems.Count == 1;
			    return !_fileService.IsDeletionInProgress && numberOfSelectedItemsAllowed;
		    }
	    }

	    public bool IsDeleteActionHidden
		    => !configuration.ActionsConfiguration.CanRemoveDirectories && !configuration.ActionsConfiguration.CanRemoveFiles;


		public string DeleteActionTitle => "Delete";

	    public bool IsDeleteActionEnabled
	    {
		    get
		    {
				if (_selectedItems.Count == 0 || IsDeleteActionHidden)
					return false;

				if (!configuration.ActionsConfiguration.CanRemoveDirectories && _selectedItems.Any(x => x.Type == ItemType.Directory))
					return false;

				if (!configuration.ActionsConfiguration.CanRemoveFiles && _selectedItems.Any(x => x.Type == ItemType.File))
					return false;

			    return !_fileService.IsDeletionInProgress;
		    }
	    }

	    public IEnumerable<NSIndexPath> IndexPathsOfSelectedCells => _selectedItems.Select(IndexFor);

	    public void Select(NSIndexPath path)
	    {
		    var item = ItemFor(path);
		    if (IsEditing)
			    _selectedItems.Add(item);
		    else
			    Delegate?.ItemSelected(item);

		    Delegate?.Changed(this);
	    }

	    public void Deselect(NSIndexPath atIndexPath)
	    {
		    var item = ItemFor(atIndexPath);
		    if (IsEditing)
			    _selectedItems.Remove(item);
		    else
			    Delegate?.ItemSelected(item);

		    Delegate?.Changed(this);
	    }

	    public void DeleteItems(IEnumerable<NSIndexPath> atIndexPaths, Action<DeleteResult<object>> onRemovedAction)
	    {
		    var items = atIndexPaths.Select(ItemFor).ToList();
		    _fileService.Delete(items, x =>
		    {
			    onRemovedAction(x);
			    Delegate?.Changed(this);
		    });
		    Delegate?.Changed(this);
	    }

	    public void ChooseItems(Action<IEnumerable<Item<object>>> pickedItemsAction)
	    {
		    pickedItemsAction(_selectedItems);
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

	    private void Remove(Item<object> item)
	    {
		    _itemsToDisplay.Remove(item);
		    _allItems.Remove(item);
		    _selectedItems.Remove(item);
	    }

	    private NSIndexPath IndexFor(Item<object> item)
	    {
		    var itemWithIndex = _allItems.Select((x, index) => new
		    {
			    Item = x,
			    Index = index
		    }).FirstOrDefault(x => x.Item == item);

		    if (itemWithIndex == null)
			    throw new InvalidOperationException("Item does not exist.");

		    return NSIndexPath.FromItemSection(itemWithIndex.Index, 0);
	    }

	    public int NumberOfItems(int section)
	    {
		    return _itemsToDisplay.Count();
	    }

	    public ItemViewModel ItemViewModelFor(NSIndexPath indexPath)
	    {
		    var item = ItemFor(indexPath);
		    return new ItemViewModel(item, _fileSpecifications.GetFileSpecificationProvider<object>(item));
	    }

	    public Item<object> ItemFor(NSIndexPath indexPath)
		    => _itemsToDisplay[(int)indexPath.Item];

		public IDirectoryContentViewModelDelegate Delegate { get; set; }
    }

	public interface IDirectoryContentViewModelDelegate
	{
		void ListChanged(DirectoryContentViewModel viewModel);

		void Changed(DirectoryContentViewModel viewModel);

		void ItemSelected(Item<object> selectedItem);
	}
}