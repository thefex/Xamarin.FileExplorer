using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public interface IFileService
	{
		Task<LoadedItem<object>> Load(Item<object> item);

		void Delete(IEnumerable<Item<object>> itemsToDelete, Action<LoadedItem<object>> completionHandler);

		bool IsDeletionInProgress { get; }
	}
}