using Refactora.Validation.Specification.Common.Regex;

namespace Refactora.Validation.Specification.Common.Email
{
	public interface IEmailFormatSpecification : IRegexSpecification
	{
	}

	public interface IEmailFormatSpecification<TEntityType> : IRegexSpecification<TEntityType>, IEmailFormatSpecification
	{
	}
}
