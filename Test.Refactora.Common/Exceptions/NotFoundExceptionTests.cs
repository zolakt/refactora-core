using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Common.Exceptions;
using System;

namespace Test.Refactora.Common.Exceptions
{
	[TestClass]
	public class NotFoundExceptionTests
	{
		[TestMethod]
		public void NotFoundExceptionConstructorTest()
		{
			var ex = new NotFoundException();
			Assert.AreEqual(NotFoundException.DEFAULT_MESSAGE, ex.Message);
			Assert.IsInstanceOfType(ex, typeof(Exception));
			Assert.IsInstanceOfType(ex, typeof(ServiceException));

			var ex2 = new NotFoundException("test");
			Assert.AreEqual("test", ex2.Message);
			Assert.IsInstanceOfType(ex2, typeof(Exception));
			Assert.IsInstanceOfType(ex2, typeof(ServiceException));

			var ex3 = new NotFoundException(new string[] { "test", "test2" });
			Assert.AreEqual("test, test2", ex3.Message);
			Assert.IsInstanceOfType(ex3, typeof(Exception));
			Assert.IsInstanceOfType(ex3, typeof(ServiceException));
		}
	}
}
