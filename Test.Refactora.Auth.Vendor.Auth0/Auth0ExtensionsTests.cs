using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Auth;
using Refactora.Auth.Management;
using Refactora.Auth.Vendor.Auth0;

namespace Test.Refactora.Auth.Vendor.Auth0
{
	[TestClass]
	public class AuthExtensionsTests
	{
		const string _host = "refactora.eu.auth0.com";
		const string _cliendId = "WECk233zrRcm1EGOSNgpAKfqGTn1C8zH";
		const string _clientSecret = "a6IMI8wbcJ7mcUx0lD-VbsAwVL_GQz3hfYiMiXb5lbgA9m4StwlCt8iEM9E0mdXf";
		const string _audience = "https://refactora.eu.auth0.com/api/v2/";

		[TestMethod]
		public void Auth0ExtensionsDiBindingTest()
		{
			var serviceCollection = new ServiceCollection();
			var builder = new MvcCoreBuilder(serviceCollection, new ApplicationPartManager());

			builder.AddAuth0Module(_host, _cliendId, _clientSecret, _audience);

			var provider = serviceCollection.BuildServiceProvider();
			Assert.IsInstanceOfType(provider.GetService<IAuthClient>(), typeof(Auth0Client));
			Assert.IsInstanceOfType(provider.GetService<IExternalUserManager>(), typeof(Auth0UserManager));
		}
	}
}
