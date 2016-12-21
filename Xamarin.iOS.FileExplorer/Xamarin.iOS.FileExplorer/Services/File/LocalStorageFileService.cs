using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreFoundation;
using Foundation;
using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public class LocalStorageFileService<T> : IFileService<T>
	{
		private readonly NSFileManager _fileManager;

		public LocalStorageFileService() : this(NSFileManager.DefaultManager)
		{
			
		}

		public LocalStorageFileService(NSFileManager fileManager)
		{
			_fileManager = fileManager;
		}

		public Result<LoadedItem<T>> Load(Item<T> item)
		{
			T resultObject = default(T);
			NSFileAttributes attributes = null;
			 
			attributes = _fileManager.GetAttributes(item.Url.Path);

			if (item.Type == ItemType.Directory)
			{
				NSString[] properties = new[] {NSUrl.IsDirectoryKey, NSUrl.ContentModificationDateKey};
				NSError error = null;
				var urls = _fileManager.GetDirectoryContent(item.Url, NSArray.FromNSObjects(properties),
					NSDirectoryEnumerationOptions.SkipsHiddenFiles,
					out error);

				resultObject = item.Parse(attributes, null, urls);
			}
			else
			{
				var data = NSData.FromUrl(item.Url);
				resultObject = item.Parse(attributes, data, null);
			}

			return new Result<LoadedItem<T>>(new LoadedItem<T>(item, attributes, resultObject));
		}

		public void Delete(IEnumerable<Item<T>> itemsToDelete, Action<LoadedItem<T>> completionHandler)
		{
			if (IsDeletionInProgress)
				return;

			IsDeletionInProgress = true;

			IList<Item<T>> deletedItems = new List<Item<T>>();
			IList<Item<T>> itemsNotDeletedDueToFailure = new List<Item<T>>();


			DispatchQueue.DefaultGlobalQueue.DispatchAsync(() =>
			{
				foreach (var item in itemsToDelete)
				{
					try
					{
						NSError error;
						_fileManager.Remove(item.Url, out error);
						deletedItems.Remove(item);
					}
					catch (Exception e)
					{
						itemsNotDeletedDueToFailure.Add(item);
					}
				}
			});

			DispatchQueue.MainQueue.DispatchAsync(() =>
			{
				if (deletedItems.Count > 0)
				{
					NSNotificationCenter.DefaultCenter.PostNotification(NSNotification.FromName("ItemsDeleted",
						NSArray.FromObject(deletedItems.ToArray())));
				}
				IsDeletionInProgress = false;

				if (itemsNotDeletedDueToFailure.Count > 0)
				{
					
				}
			});
		}

		public bool IsDeletionInProgress { get; private set; }
	}
}