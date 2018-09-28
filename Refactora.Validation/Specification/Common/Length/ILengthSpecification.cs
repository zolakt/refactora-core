namespace Refactora.Validation.Specification.Common.Length
{
	public interface ILenghtSpecification
	{
		int Min { get; }

		int Max { get; }
	}

	public interface ILenghtSpecification<TEntityType> : ISpecification<TEntityType>, ILenghtSpecification
	{
	}
}
