using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Auth.Vendor.Auth0;

namespace Test.Refactora.Auth.Vendor.Auth0
{
	[TestClass]
    public class Auth0ClientTests
	{
		const string _apiHost = "refactora.eu.auth0.com";
		const string _cliendId = "WECk233zrRcm1EGOSNgpAKfqGTn1C8zH";
		const string _clientSecret = "a6IMI8wbcJ7mcUx0lD-VbsAwVL_GQz3hfYiMiXb5lbgA9m4StwlCt8iEM9E0mdXf";
		const string _audience = "https://refactora.eu.auth0.com/api/v2/";

		[TestMethod]
		public void GetAuth0ClientTest()
		{
			var auth0 = new Auth0Client(_apiHost, _cliendId, _clientSecret, _audience);
			Assert.IsNotNull(auth0);
		}
	}
}
