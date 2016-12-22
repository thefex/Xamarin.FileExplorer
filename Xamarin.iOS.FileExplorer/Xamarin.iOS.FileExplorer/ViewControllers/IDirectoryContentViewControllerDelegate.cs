using System.Collections.Generic;
using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
    public interface IDirectoryContentViewControllerDelegate
    {
        void ChangedEditingStatus(DirectoryContentViewController controller, bool isEditing);
        void SelectedItem(DirectoryContentViewController controller, Item<object> item);
        void SelectedItemDetails(DirectoryContentViewController controller, Item<object> item);
        void ChoosedItems(DirectoryContentViewController controller, IEnumerable<Item<object>> items);
    }
}