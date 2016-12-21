using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.iOS.FileExplorer.Data;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public interface IFileService<T>
	{
		Result<LoadedItem<T>> Load(Item<T> item);

		void Delete(IEnumerable<Item<T>> itemsToDelete, Action<LoadedItem<T>> completionHandler);

		bool IsDeletionInProgress { get; }
	}
}