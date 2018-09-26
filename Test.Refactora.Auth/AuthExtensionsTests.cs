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
		private readonly string _host = "";
		private readonly string _clientHost = "";
		private readonly string _audience = "";

		[TestMethod]
		public void AuthExtensionsDiBindingTest()
		{
			var builder = GetBuilder(false).AddDefaultAuth(_host, _clientHost, _audience);
			var provider = builder.Services.BuildServiceProvider();

			Assert.IsInstanceOfType(provider.GetService<IHttpContextAccessor>(), typeof(HttpContextAccessor));
			Assert.IsInstanceOfType(provider.GetService<IContactDetails>(), typeof(ContactDetails));
			Assert.IsInstanceOfType(provider.GetService<IAuthProvider>(), typeof(IdentityAuthProvider));
		}

		[TestMethod]
		public void AuthExtensionsGenericProviderDiBindingTest()
		{
			var builder = GetBuilder().AddDefaultAuth<FakeUser>(_host, _clientHost, _audience);
			var provider = builder.Services.BuildServiceProvider();
			Assert.IsInstanceOfType(provider.GetService<IAuthProvider<FakeUser>>(), typeof(IdentityAuthProvider<FakeUser>));

			var builder2 = GetBuilder().AddDefaultAuth<FakeUser, string>(_host, _clientHost, _audience);
			var provider2 = builder2.Services.BuildServiceProvider();
			Assert.IsInstanceOfType(provider2.GetService<IAuthProvider<FakeUser, string>>(), typeof(IdentityAuthProvider<FakeUser, string>));
		}

		[TestMethod]
		public void AuthExtensionsCustomProviderDiBindingTest()
		{
			var builder = GetBuilder().AddCustomAuth<FakeAuthProvider>(_host, _clientHost, _audience);
			var provider = builder.Services.BuildServiceProvider();
			Assert.IsInstanceOfType(provider.GetService<IAuthProvider>(), typeof(FakeAuthProvider));
			Assert.IsNull(provider.GetService<IAuthProvider<FakeUser>>());
			Assert.IsNull(provider.GetService<IAuthProvider<FakeUser, string>>());

			var builder2 = GetBuilder().AddCustomAuth<FakeAuthProvider, IFakeAuthProvider>(_host, _clientHost, _audience);
			var provider2 = builder2.Services.BuildServiceProvider();
			Assert.IsInstanceOfType(provider2.GetService<IAuthProvider>(), typeof(FakeAuthProvider));
			Assert.IsInstanceOfType(provider2.GetService<IFakeAuthProvider>(), typeof(FakeAuthProvider));
			Assert.IsNull(provider2.GetService<IAuthProvider<FakeUser>>());
			Assert.IsNull(provider2.GetService<IAuthProvider<FakeUser, string>>());
		}


		private MvcCoreBuilder GetBuilder(bool withMapper = true)
		{
			var serviceCollection = new ServiceCollection();

			if (withMapper)
			{
				var mapper = new Mock<IDataMapper>();
				mapper.Setup(x => x.Map<ClaimsPrincipal, FakeUser>(It.IsAny<ClaimsPrincipal>())).Returns(new FakeUser());

				serviceCollection.AddTransient<IDataMapper>(x => mapper.Object);
			}

			return new MvcCoreBuilder(serviceCollection, new ApplicationPartManager());
		}
	}
}
