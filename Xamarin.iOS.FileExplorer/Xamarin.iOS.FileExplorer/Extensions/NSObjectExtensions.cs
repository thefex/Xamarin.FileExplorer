using System;
using System.Drawing;
using System.Globalization;
using Foundation;

namespace Xamarin.iOS.FileExplorer.Extensions
{
	public static class NSObjectExtensions
	{
		public static Object ToObject(this NSObject nsO, Type targetType)
		{
			if (nsO is NSString)
			{
				return nsO.ToString();
			}

			if (nsO is NSDate)
			{
				var nsDate = (NSDate)nsO;
				throw new NotImplementedException("NSDate is not implemented yet.");
			}

			if (nsO is NSDecimalNumber)
			{
				return decimal.Parse(nsO.ToString(), CultureInfo.InvariantCulture);
			}

			if (nsO is NSNumber)
			{
				var x = (NSNumber)nsO;

				switch (Type.GetTypeCode(targetType))
				{
					case TypeCode.Boolean:
						return x.BoolValue;
					case TypeCode.Char:
						return Convert.ToChar(x.ByteValue);
					case TypeCode.SByte:
						return x.SByteValue;
					case TypeCode.Byte:
						return x.ByteValue;
					case TypeCode.Int16:
						return x.Int16Value;
					case TypeCode.UInt16:
						return x.UInt16Value;
					case TypeCode.Int32:
						return x.Int32Value;
					case TypeCode.UInt32:
						return x.UInt32Value;
					case TypeCode.Int64:
						return x.Int64Value;
					case TypeCode.UInt64:
						return x.UInt64Value;
					case TypeCode.Single:
						return x.FloatValue;
					case TypeCode.Double:
						return x.DoubleValue;
				}
			}

			if (nsO is NSValue)
			{
				var v = (NSValue)nsO;

				if (targetType == typeof(IntPtr))
				{
					return v.PointerValue;
				}

				if (targetType == typeof(SizeF))
				{
					return v.SizeFValue;
				}

				if (targetType == typeof(RectangleF))
				{
					return v.RectangleFValue;
				}

				if (targetType == typeof(PointF))
				{
					return v.PointFValue;
				}
			}

			return nsO;
		}
	}
}