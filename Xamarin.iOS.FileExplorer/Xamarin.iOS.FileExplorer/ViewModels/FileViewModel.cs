using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.CustomViews;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;
using static System.String;

namespace Xamarin.iOS.FileExplorer.ViewModels
{
    public class FileViewModel
    {
        public string Title;
        private LoadedItem<object> item;
        private IFileSpecificationProvider specification;
        private IEnumerable<FileAttributeViewModel> items;
        private static NSByteCountFormatter sizeFormatter => new NSByteCountFormatter();

        public FileViewModel(LoadedItem<object> item, IFileSpecificationProvider specification)
        {
            this.item = item;
            this.Title = item.Name;
            this.specification = specification;

            this.items = new List<FileAttributeViewModel>
            {
                MakeFileSizeItem(this.item.Attributes),
                MakeCreationDateItem(this.item.Attributes),
                MakeModificationDateItem(this.item.Attributes)
            };
        }

        public UIImage ThumbnailImage(CGSize size)
        {
            switch (item.Type)
            {
                case ItemType.File:
                    return specification.GetThumbnail(item.Url, size) ?? ImageAssets.GenericDocumentIcon;
                default:
                    return ImageAssets.GenericDirectoryIcon;
            }
        }

        public int NumberOfAttributes => items.Count();

        public FileAttributeViewModel Atrribute(int item)
        {
            return items.ElementAt(item);
        }

        private static FileAttributeViewModel MakeFileSizeItem(NSFileAttributes attributes)
        {
            var byteCount = attributes.Size ?? 0;
            var keyLabel = NSBundle.MainBundle.LocalizedString("Size", Empty);
            if (keyLabel == null)
            {
                return null;
            }
            return new FileAttributeViewModel(keyLabel, sizeFormatter.Format(Convert.ToInt64(byteCount)));
        }

        private static FileAttributeViewModel MakeCreationDateItem(NSFileAttributes attributes)
        {
            var creationDate = attributes.CreationDate;
            var keyLabel = NSBundle.MainBundle.LocalizedString("Created", Empty);
            if (keyLabel == null)
            {
                return null;
            }
            return new FileAttributeViewModel(keyLabel,
                NSDateFormatter.ToLocalizedString(creationDate, NSDateFormatterStyle.Medium, NSDateFormatterStyle.Medium));
        }

        private static FileAttributeViewModel MakeModificationDateItem(NSFileAttributes attributes)
        {
            var modificationDate = attributes.ModificationDate;
            var keyLabel = NSBundle.MainBundle.LocalizedString("Modified", Empty);
            if (keyLabel == null)
            {
                return null;
            }
            return new FileAttributeViewModel(keyLabel,
                NSDateFormatter.ToLocalizedString(modificationDate, NSDateFormatterStyle.Medium, NSDateFormatterStyle.Medium));
        }

        private static FileAttributeViewModel MakeFileTypeItem(NSFileAttributes attributes)
        {
            string fileType = attributes?.Type?.ToString() ?? Empty;
            var keyLabel = NSBundle.MainBundle.LocalizedString("Kind", Empty);
            if (keyLabel == null)
            {
                return null;
            }
            return new FileAttributeViewModel(keyLabel, fileType);
        }
    }
}