using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactora.Common.Mapper;
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
			var mapper = new Mock<IDataMapper>();

			var provider = new FakeAuthProvider(http.Object, mapper.Object);
			Assert.IsFalse(provider.IsAuthenticated);

			var provider2 = new FakeAuthProvider(http.Object, mapper.Object, true);
			Assert.IsTrue(provider2.IsAuthenticated);
		}
	}
}
