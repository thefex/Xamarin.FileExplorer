namespace Xamarin.iOS.FileExplorer.Data
{
	public class Result<T>
	{
		public bool IsSuccess => ResultObject != null;

		public T ResultObject { get; }

		public string ErrorMessage { get; private set; } = string.Empty;

		private Result()
		{
			
		}

		public Result(T result)
		{
			ResultObject = result;
		}

		public static Result<T> BuildFailedResult(string errorMessage = "") => new Result<T>()
		{
			ErrorMessage = errorMessage
		};
	}
}