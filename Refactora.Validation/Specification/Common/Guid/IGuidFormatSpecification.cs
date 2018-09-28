using Refactora.Validation.Specification.Common.Regex;

namespace Refactora.Validation.Specification.Common.Guid
{
	public interface IGuidFormatSpecification : IRegexSpecification
	{
	}

	public interface IGuidFormatSpecification<TEntityType> : IRegexSpecification<TEntityType>, IGuidFormatSpecification
	{
	}
}
