using Refactora.Validation.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Refactora.Validation.Manager
{
	public class ValidationManger<TKey, TEntityType> : IValidationManager<TKey, TEntityType>
		where TEntityType : class
	{
		private IDictionary<TKey, IValidator<TEntityType>> _validators;

		public ValidationManger()
		{
			_validators = new Dictionary<TKey, IValidator<TEntityType>>();
		}

		public void RegisterValidator(TKey type, IValidator<TEntityType> validator)
		{
			_validators[type] = validator;
		}

		public async Task ValidateAsync(TKey type, TEntityType entity = null)
		{
			await _validators[type]?.ValidateAsync(entity);
		}
	}
}
