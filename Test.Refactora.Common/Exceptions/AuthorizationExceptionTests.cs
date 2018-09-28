using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Common.Exceptions;
using System;

namespace Test.Refactora.Common.Exceptions
{
	[TestClass]
	public class AuthorizationExceptionTests
	{
		[TestMethod]
		public void AuthorizationExceptionConstructorTest()
		{
			var ex = new AuthorizationException();
			Assert.AreEqual(AuthorizationException.DEFAULT_MESSAGE, ex.Message);
			Assert.IsInstanceOfType(ex, typeof(Exception));
			Assert.IsInstanceOfType(ex, typeof(ServiceException));

			var ex2 = new AuthorizationException("test");
			Assert.AreEqual("test", ex2.Message);
			Assert.IsInstanceOfType(ex2, typeof(Exception));
			Assert.IsInstanceOfType(ex2, typeof(ServiceException));

			var ex3 = new AuthorizationException(new string[] { "test", "test2" });
			Assert.AreEqual("test, test2", ex3.Message);
			Assert.IsInstanceOfType(ex3, typeof(Exception));
			Assert.IsInstanceOfType(ex3, typeof(ServiceException));
		}
	}
}
