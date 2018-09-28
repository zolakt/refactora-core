using System.Collections.Generic;

namespace Refactora.Validation.Rules
{
	public class ValidationRule : IValidationRule
	{
		public ValidationRule(string description, string tag = null) :
			this(description, !string.IsNullOrEmpty(tag) ? new string[] { tag } : null) { }

		public ValidationRule(string description, string[] tags)
		{
			Description = description;
			Tags = tags;
		}


		public string Description { get; }

		public IEnumerable<string> Tags { get; }
	}
}
