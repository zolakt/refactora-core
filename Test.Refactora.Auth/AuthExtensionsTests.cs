using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactora.Auth;
using Refactora.Auth.Common;
using Refactora.Auth.Provider;
using Refactora.Common.Mapper;
using System.Security.Claims;
using Test.Refactora.Auth.Fakes;

namespace Test.Refactora.Auth
{
	[TestClass]
	public class AuthExtensionsTests
	{
		[TestMethod]
		public void AuthExtensionsDiBindingTest()
		{
			var host = "";
			var clientHost = "";
			var audience = "";

			var serviceCollection = new ServiceCollection();
			var builder = new MvcCoreBuilder(serviceCollection, new ApplicationPartManager());

			builder.AddAuthModule(host, clientHost, audience);

			var provider = serviceCollection.BuildServiceProvider();
			Assert.IsInstanceOfType(provider.GetService<IHttpContextAccessor>(), typeof(HttpContextAccessor));
			Assert.IsInstanceOfType(provider.GetService<IContactDetails>(), typeof(ContactDetails));
			Assert.IsInstanceOfType(provider.GetService<IAuthProvider>(), typeof(IdentityAuthProvider));
		}

		[TestMethod]
		public void AuthExtensionsCustomProviderDiBindingTest()
		{
			var host = "";
			var clientHost = "";
			var audience = "";

			var mapper = new Mock<IDataMapper>();
			mapper.Setup(x => x.Map<ClaimsPrincipal, FakeUser>(It.IsAny<ClaimsPrincipal>())).Returns(new FakeUser());

			var serviceCollection = new ServiceCollection();
			serviceCollection.AddTransient<IDataMapper>(x => mapper.Object);

			var builder = new MvcCoreBuilder(serviceCollection, new ApplicationPartManager());

			builder.AddAuthModule<IFakeAuthProvider, FakeAuthProvider>(host, clientHost, audience);

			var provider = serviceCollection.BuildServiceProvider();
			Assert.IsInstanceOfType(provider.GetService<IHttpContextAccessor>(), typeof(HttpContextAccessor));
			Assert.IsInstanceOfType(provider.GetService<IContactDetails>(), typeof(ContactDetails));
			Assert.IsInstanceOfType(provider.GetService<IAuthProvider>(), typeof(IdentityAuthProvider));
			Assert.IsInstanceOfType(provider.GetService<IFakeAuthProvider>(), typeof(FakeAuthProvider));
		}
	}
}
