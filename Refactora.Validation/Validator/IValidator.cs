using Refactora.Validation.Rules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Refactora.Validation.Validator
{
	public interface IValidator<in TEntityType>
	{
		/// <summary>
		/// Check if entity valid
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>
		/// true/false
		/// </returns>
		Task<bool> IsValidAsync(TEntityType entity);

		/// <summary>
		/// Get broken validation rules
		/// Empty if entity is valid</summary>
		/// <param name="entity"></param>
		/// <returns>
		/// Array of broken rules or empty
		/// </returns>
		Task<IEnumerable<IBusinessRule>> GetBrokenRulesAsync(TEntityType entity);

		/// <summary>
		/// Validate entity and throw exception if invalid
		/// </summary>
		/// <param name="entity"></param>
		/// <exception cref="System.Exception"></exception>
		Task ValidateAsync(TEntityType entity);
	}
}
