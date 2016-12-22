using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
    public interface IDirectoryContentViewControllerDelegate
    {
        void DirectoryContentViewControllerChangedEditingStatus(DirectoryContentViewController controller, bool isEditing);
        void DirectoryContentViewControllerSelectedItem(DirectoryContentViewController controller, Item<object> item);
        void DirectoryContentViewControllerSelectedItemDetails(DirectoryContentViewController controller, Item<object> item);
        void DirectoryContentViewControllerChoosedItems(DirectoryContentViewController controller, Item<object> item);
    }
}