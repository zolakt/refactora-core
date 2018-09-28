using Refactora.Common.Extensions;
using Refactora.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Refactora.Validation.Specification.Common.Regex
{
	public class RegexSpecification<TEntityType> : IRegexSpecification<TEntityType>
	{
		protected readonly IEnumerable<IBusinessRule> _rules;
		protected readonly PropertyInfo _propertyInfo;

		public RegexSpecification(Expression<Func<TEntityType, string>> field, string regex, 
			string description = null, string tag = null) :
			this(field, regex, description, !string.IsNullOrEmpty(tag) ? new [] { tag } : new string[] { }) { }

		public RegexSpecification(Expression<Func<TEntityType, string>> field, string regex, 
			string description, string[] tags)
		{
			Format = regex ?? throw new ArgumentNullException("regex");

			_propertyInfo = field.GetPropertyInfo();

			tags = tags.Any() ? tags : new[] { _propertyInfo.Name };
			_rules = new[] { new ValidationRule(description ?? _propertyInfo.Name + " invalid format", tags) };
		}

		public string Format { get; }

		public async Task<IEnumerable<IBusinessRule>> GetBrokenRulesAsync(TEntityType entity = default(TEntityType))
		{
			var value = _propertyInfo.GetValue(entity) as string;

			return await Task.FromResult(!System.Text.RegularExpressions.Regex.IsMatch(value, Format, RegexOptions.IgnoreCase)
				? _rules : new IBusinessRule[] { });
		}
	}
}
