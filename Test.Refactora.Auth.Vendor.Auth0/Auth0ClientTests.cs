using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Auth.Vendor.Auth0;
using System;

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
		public void Auth0ClientConstructorTest()
		{
			var auth0 = new Auth0Client(_apiHost, _cliendId, _clientSecret, null);
			Assert.IsNotNull(auth0);
		}

		[TestMethod]
		public void Auth0ClientConfigTest()
		{
			TestConfig("");
			TestConfig(null);
		}

		private void TestConfig(string emptyValue)
		{
			var emptyMessage = "empty: {0}, value: " + ((emptyValue == "") ? "''" : "null");

			try
			{
				var auth0 = new Auth0Client(emptyValue, emptyValue, emptyValue, emptyValue);
			}
			catch (Exception)
			{
				Assert.IsTrue(true, string.Format(emptyMessage, "all"));
			}

			try
			{
				var auth0 = new Auth0Client(_apiHost, emptyValue, emptyValue, emptyValue);
			}
			catch (Exception)
			{
				Assert.IsTrue(true, string.Format(emptyMessage, "id, secret, audience"));
			}

			try
			{
				var auth0 = new Auth0Client(_apiHost, _cliendId, emptyValue, emptyValue);
			}
			catch (Exception)
			{
				Assert.IsTrue(true, string.Format(emptyMessage, "secret, audience"));
			}

			try
			{
				var auth0 = new Auth0Client(emptyValue, _cliendId, _clientSecret, emptyValue);
			}
			catch (Exception)
			{
				Assert.IsTrue(true, string.Format(emptyMessage, "host, audience"));
			}

			var valid = new Auth0Client(_apiHost, _cliendId, _clientSecret, emptyValue);
			Assert.IsNotNull(valid);
		}
	}
}
