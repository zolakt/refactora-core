using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Auth.Common;
using Refactora.Auth.Vendor.Auth0;
using System.Threading.Tasks;

namespace Test.Refactora.Auth.Vendor.Auth0
{
	[TestClass]
    public class Auth0UserManagerTests
	{
		const string _apiHost = "refactora.eu.auth0.com";
		const string _cliendId = "WECk233zrRcm1EGOSNgpAKfqGTn1C8zH";
		const string _clientSecret = "a6IMI8wbcJ7mcUx0lD-VbsAwVL_GQz3hfYiMiXb5lbgA9m4StwlCt8iEM9E0mdXf";
		const string _audience = "https://refactora.eu.auth0.com/api/v2/";

		[TestMethod]
		[Ignore]
		public async Task CreateUserTest()
		{
			var auth0 = new Auth0Client(_apiHost, _cliendId, _clientSecret, _audience);
			var manager = new Auth0UserManager(auth0);

			var details = new ContactDetails
			{
				FirstName = "Zoran",
				LastName = "Ivancevic",
				Address = "Test",
				Phone = "123"
			};

			var result1 = await manager.CreateUser("zolakt@gmail.com", details, "test");
			Assert.IsFalse(result1);

			var result2 = await manager.CreateUser("zolakt2@gmail.com", details, "test");
			Assert.IsTrue(result2);

			var result3 = await manager.DeleteUser("zolakt2@gmail.com");
			Assert.IsTrue(result3);
		}

		[TestMethod]
		[Ignore]
		public async Task GetChangePasswordUrlTest()
		{
			var auth0 = new Auth0Client(_apiHost, _cliendId, _clientSecret, _audience);
			var manager = new Auth0UserManager(auth0);

			var result1 = await manager.GetChangePasswordUrl("zolakt@gmail.com", "http://localhost:5002");
			Assert.IsNull(result1);

			var result2 = await manager.GetChangePasswordUrl("zolakt@refactora.com", "http://localhost:5002");
			Assert.IsNotNull(result2);
		}
	}
}
