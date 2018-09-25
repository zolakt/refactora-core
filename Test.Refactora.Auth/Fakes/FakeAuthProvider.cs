using Microsoft.AspNetCore.Http;
using Moq;
using Refactora.Auth.Provider;
using Refactora.Common.Mapper;
using System.Security.Claims;

namespace Test.Refactora.Auth.Fakes
{
	public class FakeAuthProvider : IdentityAuthProvider<FakeUser>, IFakeAuthProvider
	{
		private readonly bool _isAuthorized;

		public FakeAuthProvider(IHttpContextAccessor contextAccessor, IDataMapper mapper, bool authorized = false)
			: base(contextAccessor, mapper)
		{
			_isAuthorized = authorized;
		}

		public bool HasPermission(object permission)
		{
			return true;
		}

		protected override ClaimsPrincipal GetClaimsUser()
		{
			var identity = new Mock<ClaimsPrincipal>();
			identity.Setup(x => x.Identity.IsAuthenticated).Returns(_isAuthorized);
			return identity.Object;
		}
	}
}
