using Refactora.Validation.Specification.Common.Regex;

namespace Refactora.Validation.Specification.Common.Phone
{
	public interface IPhoneFormatSpecification : IRegexSpecification
	{
	}

	public interface IPhoneFormatSpecification<TEntityType> : IRegexSpecification<TEntityType>, IPhoneFormatSpecification
	{
	}
}
