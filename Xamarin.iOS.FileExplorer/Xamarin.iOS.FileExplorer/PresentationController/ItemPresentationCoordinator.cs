using UIKit;
using Xamarin.iOS.FileExplorer.Config;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Services.File;

namespace Xamarin.iOS.FileExplorer.PresentationController
{
	public class ItemPresentationCoordinator
	{
		private Configuration configuration;
		private FileSpecifications fileSpecifications;
		

		public ItemPresentationCoordinator(UINavigationController navigationController)
		{
			
		}

		public void Start(Item<object> item, FileSpecifications fileSpecifications, Configuration configuration, bool animated)
		{
			this.configuration = configuration;
			this.fileSpecifications = fileSpecifications;

			switch (item.Type)
			{
				//case ItemType.Directory:
				//	var coordinator = new FileItemPresentationCoordinator(n)

			}
		}
	}
}