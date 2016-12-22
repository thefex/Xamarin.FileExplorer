namespace Xamarin.iOS.FileExplorer.ViewModels
{
    public class FileAttributeViewModel
    {
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }

        public FileAttributeViewModel(string attributeName, string attributeValue)
        {
            AttributeName = attributeName;
            AttributeValue = attributeValue;
        }
    }
}