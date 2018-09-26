using Microsoft.AspNetCore.Http;
using Refactora.Common.Mapper;
using System;
using System.Security.Claims;

namespace Refactora.Auth.Provider
{
	public class IdentityAuthProvider : IAuthProvider
	{
		protected readonly IHttpContextAccessor _contextAccessor;

		public IdentityAuthProvider(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor ?? throw new ArgumentNullException("contextAccessor");
		}

		public virtual bool IsAuthenticated
		{
			get { return IdentityUser?.Identity?.IsAuthenticated ?? false; }
		}

		protected virtual ClaimsPrincipal IdentityUser
		{
			get	{ return _contextAccessor?.HttpContext?.User; }
		}
	}

	public class IdentityAuthProvider<TEntityType> : IdentityAuthProvider, IAuthProvider<TEntityType>
	{
		protected readonly IDataMapper _mapper;

		public IdentityAuthProvider(IHttpContextAccessor contextAccessor, IDataMapper mapper) : base(contextAccessor)
		{
			_mapper = mapper ?? throw new ArgumentNullException("mapper");
		}

		public virtual TEntityType CurrentUser
		{
			get { return _mapper.Map<TEntityType>(IdentityUser); }
		}
	}


	public class IdentityAuthProvider<TEntityType, TPermissionType> : IdentityAuthProvider<TEntityType>, IAuthProvider<TEntityType, TPermissionType>
	{
		public IdentityAuthProvider(IHttpContextAccessor contextAccessor, IDataMapper mapper) : base(contextAccessor, mapper)
		{
		}

		public virtual bool HasPermission(TPermissionType permission)
		{
			return IsAuthenticated;
		}
	}
}
