using Refactora.Validation.Rules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Refactora.Validation.Specification
{
	public interface ISpecification<TEntityType>
	{
		Task<IEnumerable<IBusinessRule>> GetBrokenRulesAsync(TEntityType entity = default(TEntityType));
	}
}
