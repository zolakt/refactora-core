using System;

namespace Refactora.Validation.Specification.Common.Range
{
	public interface IRangeSpecification<TValueType>
		where TValueType : struct,
		  IComparable<TValueType>,
		  IConvertible,
		  IEquatable<TValueType>,
		  IFormattable
	{
		TValueType Min { get; }

		TValueType Max { get; }
	}

	public interface IRangeSpecification<TEntityType, TValueType> : ISpecification<TEntityType>, IRangeSpecification<TValueType>
		where TValueType : struct,
		  IComparable,
		  IComparable<TValueType>,
		  IConvertible,
		  IEquatable<TValueType>,
		  IFormattable
	{
	}
}
