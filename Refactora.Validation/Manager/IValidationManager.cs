using Refactora.Validation.Validator;
using System.Threading.Tasks;

namespace Refactora.Validation.Manager
{
	public interface IValidationManager<TKey, TEntityType>
		where TEntityType : class
	{
		/// <summary>
		/// Register validator for specific KEY
		/// Keys can be anything: strings, enums...
		/// Keys give semantics to validator use cases
		/// </summary>
		/// <param name="type"></param>
		/// <param name="validator"></param>
		void RegisterValidator(TKey type, IValidator<TEntityType> validator);

		/// <summary>
		/// Check if entity is valid usind the validator from KEY
		/// Throws exceptions if invalid
		/// </summary>
		/// <param name="type"></param>
		/// <param name="entity"></param>
		/// <exception cref="System.Exception"></exception>
		Task ValidateAsync(TKey type, TEntityType entity = null);
	}
}
