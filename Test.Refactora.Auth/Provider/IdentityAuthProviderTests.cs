using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactora.Auth.Provider;
using Refactora.Common.Mapper;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.Refactora.Auth.Fakes;

namespace Test.Refactora.Auth.Provider
{
	[TestClass]
	public class IdentityAuthProviderTests
	{
		[TestMethod]
		public async Task IsAuthenticatedTest()
		{
			var http = new Mock<IHttpContextAccessor>();
			http.Setup(x => x.HttpContext.User.Identity.IsAuthenticated).Returns(true);

			var prov = new IdentityAuthProvider(http.Object);
			Assert.IsTrue(await prov.IsAuthenticatedAsync());
		}

		[TestMethod]
		public async Task CurrentUserTest()
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
			var result = await provider.GetCurrentUserAsync();

			Assert.IsNotNull(result);
			Assert.AreEqual(user.Id, result.Id);
		}

		[TestMethod]
		public async Task CheckPermissionTest()
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

			Assert.IsNotNull(await provider.GetCurrentUserAsync());
			Assert.IsTrue(await provider.HasPermissionAsync("test3")); // default implementation only checks IsAuthenticated

			var customProvider = new FakeAuthProvider(http.Object, mapper.Object, true);
			Assert.IsNotNull(await customProvider.GetCurrentUserAsync());
			Assert.IsFalse(await customProvider.HasPermissionAsync("test3"));
			Assert.IsTrue(await customProvider.HasPermissionAsync("test2"));
		}
	}
}
