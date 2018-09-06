namespace Refactora.Common.Mapper
{
	public interface IDataMapper
	{
		TOutType Map<TOutType>(object source);

		TOutType Map<TInType, TOutType>(TInType source);

		TOutType Map<TInType, TOutType>(TInType source, TOutType destination);
	}
}
