﻿using Microsoft.AspNetCore.Http;
using Moq;
using Refactora.Auth.Provider;
using Refactora.Common.Mapper;
using System.Linq;
using System.Security.Claims;

namespace Test.Refactora.Auth.Fakes
{
	public interface IFakeAuthProvider : IAuthProvider<FakeUser, string> { }

	public class FakeAuthProvider : IdentityAuthProvider<FakeUser>, IFakeAuthProvider
	{
		private readonly bool _isAuthorized;

		public FakeAuthProvider(IHttpContextAccessor contextAccessor, IDataMapper mapper, bool authorized = false)
			: base(contextAccessor, mapper)
		{
			_isAuthorized = authorized;
		}

		protected override ClaimsPrincipal IdentityUser
		{
			get
			{
				var identity = new Mock<ClaimsPrincipal>();
				identity.Setup(x => x.Identity.IsAuthenticated).Returns(_isAuthorized);
				return identity.Object;
			}
		}

		public bool HasPermission(string permission)
		{
			return CurrentUser?.Permissions?.Contains(permission) ?? false;
		}
	}
}
