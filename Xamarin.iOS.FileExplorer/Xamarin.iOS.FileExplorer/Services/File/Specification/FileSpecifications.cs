using System.Collections.Generic;
using System.Linq;
using Xamarin.iOS.FileExplorer.Data;
using Xamarin.iOS.FileExplorer.Extensions;

namespace Xamarin.iOS.FileExplorer.Services.File
{
	public class FileSpecifications
	{
		private readonly IFileSpecificationProvider _defaultFallbackProvider;
		private readonly IList<IFileSpecificationProvider> _fileSpecifiations = new List<IFileSpecificationProvider>();

		public FileSpecifications() : this(new DefaultFileSpecificationProvider())
		{
		}

		public FileSpecifications(IFileSpecificationProvider defaultFallBackProvider,
			params IFileSpecificationProvider[] fileSpecProvider)
		{
			var defaultProviders = new List<IFileSpecificationProvider>
			{
				new ImageSpecificationProvider(),
				new VideoSpecificationProvider(),
				new AudioSpecificationProvider(),
				new PdfSpecificationProvider(),
				new DefaultFileSpecificationProvider()
			};
			foreach (var provider in fileSpecProvider.Union(defaultProviders).Distinct())
				_fileSpecifiations.Add(provider);

			_defaultFallbackProvider = defaultFallBackProvider;
		}

		public IFileSpecificationProvider GetFileSpecificationProvider<T>(Item<T> item)
		{
			return _fileSpecifiations.FirstOrDefault(x => x.DoesDescirbeItem(item)) ?? _defaultFallbackProvider;
		}
	}
}