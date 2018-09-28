using Refactora.Validation.Rules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Refactora.Validation.Specification
{
	public interface ISpecification
	{
		IEnumerable<IBusinessRule> AvailableRules { get; }
	}

	public interface ISpecification<TEntityType> : ISpecification
	{
		Task<IEnumerable<IBusinessRule>> GetBrokenRulesAsync(TEntityType entity = default(TEntityType));
	}
}
