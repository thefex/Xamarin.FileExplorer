using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreFoundation;
using Foundation;
using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public class LocalStorageFileService : IFileService
	{
		private readonly NSFileManager _fileManager;

		public LocalStorageFileService() : this(NSFileManager.DefaultManager)
		{
			
		}

		public LocalStorageFileService(NSFileManager fileManager)
		{
			_fileManager = fileManager;
		}

		public async Task<LoadedItem<object>> Load(Item<object> item)
		{
			object resultObject = null;
			NSFileAttributes attributes = null;
			await Task.Run(() =>
			{
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
			});
			return new LoadedItem<object>(item, attributes, resultObject);
		}

		public void Delete(IEnumerable<Item<object>> itemsToDelete, Action<LoadedItem<object>> completionHandler)
		{
			if (IsDeletionInProgress)
				return;

			IsDeletionInProgress = true;

			IList<Item<object>> deletedItems = new List<Item<object>>();
			IList<Item<object>> itemsNotDeletedDueToFailure = new List<Item<object>>();


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