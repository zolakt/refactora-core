using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactora.Auth.Provider;
using Refactora.Common.Mapper;
using System.Security.Claims;
using Test.Refactora.Auth.Fakes;

namespace Test.Refactora.Auth.Provider
{
	[TestClass]
	public class IdentityAuthProviderTests
	{
		[TestMethod]
		public void IsAuthenticatedTest()
		{
			var http = new Mock<IHttpContextAccessor>();
			http.Setup(x => x.HttpContext.User.Identity.IsAuthenticated).Returns(true);

			var prov = new IdentityAuthProvider(http.Object);
			Assert.IsTrue(prov.IsAuthenticated);
		}

		[TestMethod]
		public void CurrentUserTest()
		{
			var user = new FakeUser
			{
				Id = "1",
				Name = "test"
			};

			var http = new Mock<IHttpContextAccessor>();
			http.Setup(x => x.HttpContext.User).Returns(new ClaimsPrincipal());

			var mapper = new Mock<IDataMapper>();
			mapper.Setup(x => x.Map<FakeUser>(It.IsAny<ClaimsPrincipal>())).Returns(user);

			var provider = new IdentityAuthProvider<FakeUser>(http.Object, mapper.Object);
			Assert.IsNotNull(provider.CurrentUser);
			Assert.AreEqual(user.Id, provider.CurrentUser.Id);
		}

		[TestMethod]
		public void CheckPermissionTest()
		{
			var user = new FakeUser
			{
				Id = "1",
				Name = "test",
				Permissions = new[] { "test", "test2" }
			};

			var http = new Mock<IHttpContextAccessor>();
			http.Setup(x => x.HttpContext.User.Identity.IsAuthenticated).Returns(true);

			var mapper = new Mock<IDataMapper>();
			mapper.Setup(x => x.Map<FakeUser>(It.IsAny<ClaimsPrincipal>())).Returns(user);

			var provider = new IdentityAuthProvider<FakeUser, string>(http.Object, mapper.Object);
			Assert.IsNotNull(provider.CurrentUser);
			Assert.IsTrue(provider.HasPermission("test3")); // default implementation only checks IsAuthenticated

			var customProvider = new FakeAuthProvider(http.Object, mapper.Object, true);
			Assert.IsNotNull(customProvider.CurrentUser);
			Assert.IsFalse(customProvider.HasPermission("test3"));
			Assert.IsTrue(customProvider.HasPermission("test2"));
		}
	}
}
