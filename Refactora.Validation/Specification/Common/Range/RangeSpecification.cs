using Refactora.Common.Extensions;
using Refactora.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Refactora.Validation.Specification.Common.Range
{
	public class RangeSpecification<TEntityType,TValueType> : IRangeSpecification<TEntityType, TValueType>
		where TValueType : struct,
		  IComparable,
		  IComparable<TValueType>,
		  IConvertible,
		  IEquatable<TValueType>,
		  IFormattable
	{
		protected readonly PropertyInfo _propertyInfo;

		public IEnumerable<IBusinessRule> AvailableRules { get; }

		public TValueType Min { get; }

		public TValueType Max { get; }


		public RangeSpecification(Expression<Func<TEntityType, TValueType>> field, TValueType min, TValueType max, 
			string description = null, string tag = null) : this(field, min, max, description, !string.IsNullOrEmpty(tag) ? new [] { tag } : new string[] { }) { }

		public RangeSpecification(Expression<Func<TEntityType, TValueType?>> field, TValueType min, TValueType max,
			string description = null, string tag = null) : this(field, min, max, description, !string.IsNullOrEmpty(tag) ? new[] { tag } : new string[] { }) { }

		public RangeSpecification(Expression<Func<TEntityType, TValueType>> field, TValueType min, TValueType max,
			string description, string[] tags) : this(field.GetPropertyInfo(), min, max, description, tags) { }

		public RangeSpecification(Expression<Func<TEntityType, TValueType?>> field, TValueType min, TValueType max,
			string description, string[] tags) : this(field.GetPropertyInfo(), min, max, description, tags) { }

		public RangeSpecification(PropertyInfo property, TValueType min, TValueType max, string description, string[] tags)
		{
			Min = min;
			Max = max;

			_propertyInfo = property;

			var targetDescription = description ?? _propertyInfo.Name + " not in range";
			var targetTags = tags.Any() ? tags : new[] { _propertyInfo.Name };

			tags = tags.Any() ? tags : new[] { _propertyInfo.Name };
			AvailableRules = new[] { new ValidationRule(targetDescription, targetTags) };
		}


		public async Task<IEnumerable<IBusinessRule>> GetBrokenRulesAsync(TEntityType entity = default(TEntityType))
		{
			var value = _propertyInfo.GetValue(entity) as IComparable;

			return await Task.FromResult(((value == null) || (value.CompareTo(Min) < 0) || (value.CompareTo(Max) > 0))
				? AvailableRules : new IBusinessRule[] { });
		}
	}
}
