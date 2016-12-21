using System;
using UIKit;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Extensions;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.ViewControllers
{
	public sealed class LoadingViewController<T> : UIViewController
	{
		public LoadingViewController(Func<Result<LoadedItem<T>>> load, Func<LoadedItem<T>, UIViewController> builder)
		{
			var loadedItemResult = load();

			if (!loadedItemResult.IsSuccess)
				this.PresentAlert(loadedItemResult.ErrorMessage);
			else
			{
				var contentViewController = builder(loadedItemResult.ResultObject);
				this.AddChildViewController(contentViewController);
				NavigationItem.Title = contentViewController.NavigationItem.Title;
				NavigationItem.RightBarButtonItems = contentViewController.NavigationItem.RightBarButtonItems;
				NavigationItem.LeftBarButtonItems = contentViewController.NavigationItem.LeftBarButtonItems;
				ExtendedLayoutIncludesOpaqueBars = contentViewController.ExtendedLayoutIncludesOpaqueBars;
				EdgesForExtendedLayout = contentViewController.EdgesForExtendedLayout;
			}
		}

		public static LoadingViewController<T> Build(Item<T> item, IFileService<T> fileService,
			Func<LoadedItem<T>, UIViewController> viewControllerBuilder)
		{
			return new LoadingViewController<T>(() => 
			item == null ? Result<LoadedItem<T>>.BuildFailedResult(CustomErrors.NullItemError) : fileService.Load(item), viewControllerBuilder);
		}
	}
}