using System;
using System.IO;
using CoreAnimation;
using UIKit;
using Foundation;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Extensions;
using Xamarin.iOS.FileExplorer.Services.File;
using Xamarin.iOS.FileExplorer.ViewControllers;
using Xamarin.iOS.FileExplorer.ViewModels;

namespace Xamarin.iOS.FileExplorer.PresentationController
{
	public class FileItemPresentationCoordinator : IActionsViewControllerDelegate
	{
	    private UINavigationController navigationController;
	    private IFileService<object> fileService;
	    private FileSpecifications fileSpecifications;
	    private Item<object> item;

	    public FileItemPresentationCoordinator(UINavigationController navigationController,
	        Item<object> item,
	        FileSpecifications fileSpecifications,
	        IFileService<object> fileService = null)
	    {
	        if (fileService == null)
	        {
	            fileService = new LocalStorageFileService<object>();
	        }
	        this.navigationController = navigationController;
	        this.fileService = fileService;
	        this.fileSpecifications = fileSpecifications;
	        this.item = item;
	    }

	    public void Start(bool animated)
	    {
	        switch (item.Type)
	        {
	            case ItemType.File:
	                var viewController = MakePresentingViewController(item, loadedItem =>
	                {
	                    var vc =
	                        fileSpecifications.GetFileSpecificationProvider(item)
	                            .GetViewControllerForItem(loadedItem.Url, loadedItem.Resource as NSData,
	                                loadedItem.Attributes);
	                    vc.NavigationItem.Title = item.Name;
	                    return vc;
	                });
	                navigationController?.PushViewController(viewController, animated);
	                break;
	            case ItemType.Directory:
	                throw new InvalidDataException();
	        }
	    }

		public void StartDetailsPreview(bool animated)
		{
			var fileSpecification = fileSpecifications.GetFileSpecificationProvider<object>(item);
			var viewController = MakePresentingViewController(item, loadedItem =>
			{
				return new UIViewController();
				//var viewModel = new FileViewModel(loadedItem, fileSpecification);
				//return new FileViewController
			});
		}

	    private UIViewController MakePresentingViewController(Item<object> item,
	        Func<LoadedItem<object>, UIViewController> builder)
	    {
	        var viewController = LoadingViewController<object>.Build(item, fileService, loadedItem =>
	        {
	            var contentViewController = builder(loadedItem);
	            var actionsViewController = new ActionsViewController(contentViewController)
	            {
	                ActionsViewControllerDelegate = this
	            };
	            return actionsViewController;
	        });
	        return viewController;
	    }

	    public void ActionsViewControllerDidRequestRemoval(ActionsViewController controller)
	    {
	        var activityItem = new UIActivityItemProvider(item.Url);
	        var activityViewController = new UIActivityViewController(new[] {activityItem.Item}, null);
	        navigationController?.PresentViewController(activityViewController, true, null);
	    }

	    public void ActionsViewControllerDidRequestShare(ActionsViewController controller)
	    {
	        CATransaction.Begin();
	        CATransaction.CompletionBlock = () =>
	        {
	            this.fileService.Delete(new[] {item}, result =>
	            {
	                if (!result.IsSuccess)
	                {
	                    new UIAlertController().PresentAlert(result.ErrorMessage);
	                }
	            });
	        };
            navigationController.PopViewController(true);
            CATransaction.Commit();
        }
	}
}