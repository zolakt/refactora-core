using AutoMapper;
using System;

namespace Refactora.Common.Mapper
{
	public class DataAutoMapper : IDataMapper
	{
		private readonly IMapper _mapper;

		public DataAutoMapper(IMapper mapper)
		{
			_mapper = mapper ?? throw new ArgumentNullException("mapper");
		}

		public TOutType Map<TOutType>(object source)
		{
			return _mapper.Map<TOutType>(source);
		}

		public TOutType Map<TInType, TOutType>(TInType source)
		{
			return Map<TOutType>(source);
		}

		public TOutType Map<TInType, TOutType>(TInType source, TOutType destination)
		{
			return _mapper.Map(source, destination);
		}
	}
}
