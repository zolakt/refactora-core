using System.Collections.Generic;

namespace Refactora.Validation.Rules
{
	public interface IValidationRule : IBusinessRule
	{
		IEnumerable<string> Tags { get; }
	}
}
