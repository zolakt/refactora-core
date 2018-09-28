using Refactora.Common.Exceptions;
using Refactora.Common.Extensions;
using Refactora.Validation.Rules;
using Refactora.Validation.Specification;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactora.Validation.Validator
{

	public class SpecificationValidator<TEntityType> : IValidator<TEntityType>
		where TEntityType : class
	{
		private readonly IEnumerable<ISpecification<TEntityType>> _specifications;

		public SpecificationValidator(IEnumerable<ISpecification<TEntityType>> specifications)
		{
			_specifications = specifications;
		}

		public IEnumerable<ISpecification> AvailableSpecifications
		{
			get { return _specifications; }
		}

		public async Task<bool> IsValidAsync(TEntityType entity)
		{
			return !(await GetBrokenRulesAsync(entity)).Any();
		}

		public async Task<IEnumerable<IBusinessRule>> GetBrokenRulesAsync(TEntityType entity)
		{
			return await _specifications.SelectManyAsync(x => x.GetBrokenRulesAsync(entity));
		}

		public async Task ValidateAsync(TEntityType entity = null)
		{
			var brokenRules = await GetBrokenRulesAsync(entity);

			if (brokenRules.Any())
			{
				var authRules = brokenRules.Where(x => x is IAuthRule);
				if (authRules.Any())
				{
					throw new AuthorizationException(authRules.Select(x => x.Description));
				}

				var validationRules = brokenRules.Where(x => x is IValidationRule);
				if (validationRules.Any())
				{
					throw new ValidationException(validationRules.Select(x => x.Description));
				}

				throw new ServiceException(brokenRules.Select(x => x.Description));
			}
		}
	}
}
