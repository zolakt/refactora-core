using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Common.Exceptions;
using System;

namespace Test.Refactora.Common.Exceptions
{
	[TestClass]
	public class ServiceExceptionTests
	{
		[TestMethod]
		public void ServiceExceptionConstructorTest()
		{
			var ex = new ServiceException("test");
			Assert.AreEqual("test", ex.Message);
			Assert.IsInstanceOfType(ex, typeof(Exception));
			Assert.IsInstanceOfType(ex, typeof(ServiceException));

			var ex2 = new ServiceException(new string[] { "test", "test2" });
			Assert.AreEqual("test, test2", ex2.Message);
			Assert.IsInstanceOfType(ex2, typeof(Exception));
			Assert.IsInstanceOfType(ex2, typeof(ServiceException));
		}
	}
}
