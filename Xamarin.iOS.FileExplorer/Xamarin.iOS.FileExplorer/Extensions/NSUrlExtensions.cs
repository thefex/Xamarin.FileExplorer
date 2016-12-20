using System;
using System.Linq;
using Foundation;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class NSUrlExtensions
	{
		public static readonly NSUrl DocumentDirectory =
			new NSUrl(NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User, true).First());

		public static readonly NSUrl CacheDirectory =
			new NSUrl(NSSearchPath.GetDirectories(NSSearchPathDirectory.CachesDirectory, NSSearchPathDomain.User, true).First());

		public static int Compare(this NSUrl url, NSUrl other)
		{
			return String.Compare(MakeStandarizedLastPathComponent(url), MakeStandarizedLastPathComponent(other), StringComparison.Ordinal);
		}

		public static string MakeStandarizedLastPathComponent(NSUrl url) => url.LastPathComponent.Trim();

		public static char MakeStandarizedFirstCharacterOfLastPathComponent(NSUrl url) => MakeStandarizedLastPathComponent(url).ToUpper().FirstOrDefault();
	}
}