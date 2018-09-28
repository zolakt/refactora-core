using Refactora.Validation.Rules;
using Refactora.Validation.Specification.Common.Required;
using Refactora.Validation.Validator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactora.Validation.Exporter
{
	public class JqueryValidationExporter : IValidationExporter
	{
		private const string TYPE_REQUIRED = "required";

		public async Task<object> ExportValidationRules(IValidator validator)
		{
			var result = new Dictionary<string, IDictionary<string, object>>();

			foreach (var spec in validator.AvailableSpecifications)
			{
				if (spec is IRequiredSpecification)
				{
					if (!result.ContainsKey(TYPE_REQUIRED))
					{
						result.Add(TYPE_REQUIRED, new Dictionary<string, object>());
					}

					foreach (var rule in spec.AvailableRules)
					{
						var validationRule = rule as IValidationRule;

						if (validationRule != null)
						{
							if (validationRule.Tags.Any())
							{
								foreach (var tag in validationRule.Tags)
								{
									result[TYPE_REQUIRED].Add(tag, rule.Description);
								}
							}
							else
							{
								result[TYPE_REQUIRED].Add(string.Empty, rule.Description);
							}
						}
					}

				}
			}

			return await Task.FromResult(result);
		}
	}
}
