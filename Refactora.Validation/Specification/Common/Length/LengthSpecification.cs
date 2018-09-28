using Refactora.Common.Extensions;
using Refactora.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Refactora.Validation.Specification.Common.Length
{
	public class LengthSpecification<TEntityType> : ILenghtSpecification<TEntityType>
	{
		protected readonly IEnumerable<IBusinessRule> _rules;
		protected readonly PropertyInfo _propertyInfo;

		public LengthSpecification(Expression<Func<TEntityType, object>> field, int min, int max, 
			string description = null, string tag = null) :
			this(field, min, max, description, !string.IsNullOrEmpty(tag) ? new [] { tag } : new string[] { }) { }

		public LengthSpecification(Expression<Func<TEntityType, object>> field, int min, int max,
			string description, string[] tags)
		{
			Min = min;
			Max = max;

			_propertyInfo = field.GetPropertyInfo();

			tags = tags.Any() ? tags : new[] { _propertyInfo.Name };
			_rules = new[] { new ValidationRule(description ?? _propertyInfo.Name + " lenght not in range", tags) };
		}

		public int Min { get; }

		public int Max { get; }

		public async Task<IEnumerable<IBusinessRule>> GetBrokenRulesAsync(TEntityType entity = default(TEntityType))
		{
			var value = _propertyInfo.GetValue(entity);
			var length = (value as Array)?.Length ?? (value as string)?.Length ?? 0;

			return await Task.FromResult(((length < Min) || (length > Max))
				? _rules : new IBusinessRule[] { });
		}
	}
}
