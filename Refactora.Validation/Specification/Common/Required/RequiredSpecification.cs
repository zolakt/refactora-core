﻿using Refactora.Common.Extensions;
using Refactora.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Refactora.Validation.Specification.Common.Required
{
	public class RequiredSpecification<TEntityType> : IRequiredSpecification<TEntityType>
	{
		protected readonly PropertyInfo _propertyInfo;

		public IEnumerable<IBusinessRule> AvailableRules { get; }


		public RequiredSpecification(Expression<Func<TEntityType, object>> field, string description = null, string tag = null) :
			this(field, description, !string.IsNullOrEmpty(tag) ? new [] { tag } : new string[] { }) { }

		public RequiredSpecification(Expression<Func<TEntityType, object>> field, string description, string[] tags)
		{
			_propertyInfo = field.GetPropertyInfo();

			var targetDescription = description ?? _propertyInfo.Name + " required";
			var targetTags = tags.Any() ? tags : new[] { _propertyInfo.Name };

			AvailableRules = new[] { new ValidationRule(targetDescription, targetTags) };
		}


		public async Task<IEnumerable<IBusinessRule>> GetBrokenRulesAsync(TEntityType entity = default(TEntityType))
		{
			var value = _propertyInfo.GetValue(entity);
			return await Task.FromResult(value.IsNullOrEmpty() ? AvailableRules : new IBusinessRule[] { });
		}
	}
}
