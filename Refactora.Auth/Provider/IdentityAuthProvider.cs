using Microsoft.AspNetCore.Http;
using Refactora.Common.Mapper;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Refactora.Auth.Provider
{
	public class IdentityAuthProvider : IAuthProvider
	{
		protected readonly IHttpContextAccessor _contextAccessor;

		public IdentityAuthProvider(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor ?? throw new ArgumentNullException("contextAccessor");
		}

		public virtual async Task<bool> IsAuthenticatedAsync()
		{
			return await Task.FromResult(IdentityUser?.Identity?.IsAuthenticated ?? false);
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

		public virtual async Task<TEntityType> GetCurrentUserAsync()
		{
			return await Task.FromResult(_mapper.Map<TEntityType>(IdentityUser));
		}
	}


	public class IdentityAuthProvider<TEntityType, TPermissionType> : IdentityAuthProvider<TEntityType>, IAuthProvider<TEntityType, TPermissionType>
	{
		public IdentityAuthProvider(IHttpContextAccessor contextAccessor, IDataMapper mapper) : base(contextAccessor, mapper)
		{
		}

		public virtual async Task<bool> HasPermissionAsync(TPermissionType permission)
		{
			return await IsAuthenticatedAsync();
		}
	}
}
