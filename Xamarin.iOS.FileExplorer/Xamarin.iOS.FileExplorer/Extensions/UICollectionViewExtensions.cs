using System;
using Foundation;
using UIKit;
using Xamarin.iOS.FileExplorer.CustomViews;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class UICollectionViewExtensions
	{
		public static void RegisterCell<TCell>(this UICollectionViewExtended collectionView) => collectionView.RegisterClassForCell(typeof(TCell), typeof(TCell).ToString());

		public static T DequeueReusableCell<T>(this UICollectionViewExtended collectionView, Type ofClass, NSIndexPath indexPath) where T : UICollectionViewCell
		{
			var cell = collectionView.DequeueReusableCell(ofClass.ToString(), indexPath) as T; 

			var editableCell = cell as IEditable;
			editableCell?.SetEditing(collectionView.IsEditing, false);

			return cell;
		}

		public static void RegisterFooter<TOfClass>(this UICollectionViewExtended collectionView)
		{
			Type ofClass = typeof(TOfClass);
			collectionView.RegisterClassForSupplementaryView(ofClass, UICollectionElementKindSection.Footer, ofClass.ToString());
		}

		public static T DequeueReusableFooter<T>(this UICollectionViewExtended collectionView, Type viewClass, NSIndexPath forPath)
			where T : UICollectionReusableView
		{
			return collectionView.DequeueReusableSupplementaryView(UICollectionElementKindSection.Footer, viewClass.ToString(),
				forPath) as T;
		}

		public static void RegisterHeader<TOfClass>(this UICollectionViewExtended collectionView)
		{
			Type ofClass = typeof(TOfClass);
			collectionView.RegisterClassForSupplementaryView(ofClass, UICollectionElementKindSection.Header, ofClass.ToString());
		}

		public static T DequeueReusableHeader<T>(this UICollectionViewExtended collectionView, Type viewClass, NSIndexPath forPath)
			where T : UICollectionReusableView
		{
			return collectionView.DequeueReusableSupplementaryView(UICollectionElementKindSection.Header, viewClass.ToString(),
				forPath) as T;
		}

		public static T GetHeader<T>(this UICollectionViewExtended collection, NSIndexPath atPath) where T : class
		{
			return collection.GetSupplementaryView(UICollectionElementKindSectionKey.Header, atPath) as T; 
		}

		 
	}
}