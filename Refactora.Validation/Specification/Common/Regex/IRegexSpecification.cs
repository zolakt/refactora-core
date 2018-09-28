namespace Refactora.Validation.Specification.Common.Regex
{
	public interface IRegexSpecification
	{
		string Format { get; }
	}

	public interface IRegexSpecification<TEntityType> : ISpecification<TEntityType>, IRegexSpecification
	{
	}
}
