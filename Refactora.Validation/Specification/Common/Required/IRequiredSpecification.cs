namespace Refactora.Validation.Specification.Common.Required
{
	public interface IRequiredSpecification
	{
	}

	public interface IRequiredSpecification<TEntityType> : ISpecification<TEntityType>, IRequiredSpecification
	{
	}
}
