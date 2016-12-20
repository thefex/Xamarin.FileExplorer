namespace Xamarin.iOS.FileExplorer.Data
{
	public class Result<T>
	{
		public bool IsSuccess => ResultObject != null;

		public T ResultObject { get; }

		private Result()
		{
			
		}

		public Result(T result)
		{
			ResultObject = result;
		}

		public static Result<T> BuildFailedResult() => new Result<T>();
	}
}