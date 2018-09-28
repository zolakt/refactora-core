using System;

namespace Refactora.Common.Extensions
{
	public static class GeneralExtensions
	{
		public static bool IsNullOrEmpty<TValue>(this TValue value)
		{
			if (ReferenceEquals(value, default(TValue)))
			{
				return true;
			}

			if (value is string)
			{
				return string.IsNullOrEmpty(value as string);
			}

			if (value is Array)
			{
				return (value as Array).Length == 0;
			}

			return false;
		}

		public static int Compare<TValue>(this TValue? x, TValue? y)
			where TValue: struct, IComparable<TValue>
		{
			if (!x.HasValue && !y.HasValue)
				return 0;

			if (x.HasValue && !y.HasValue)
				return 1;

			if (y.HasValue && !x.HasValue)
				return -1;

			return x.Value.CompareTo(y.Value);
		}
	}
}
