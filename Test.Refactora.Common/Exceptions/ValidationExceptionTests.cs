using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Common.Exceptions;
using System;

namespace Test.Refactora.Common.Exceptions
{
	[TestClass]
	public class ValidationExceptionTests
	{
		[TestMethod]
		public void ValidationExceptionConstructorTest()
		{
			var ex = new ValidationException();
			Assert.AreEqual(ValidationException.DEFAULT_MESSAGE, ex.Message);
			Assert.IsInstanceOfType(ex, typeof(Exception));
			Assert.IsInstanceOfType(ex, typeof(ServiceException));

			var ex2 = new ValidationException("test");
			Assert.AreEqual("test", ex2.Message);
			Assert.IsInstanceOfType(ex2, typeof(Exception));
			Assert.IsInstanceOfType(ex2, typeof(ServiceException));

			var ex3 = new ValidationException(new string[] { "test", "test2" });
			Assert.AreEqual("test, test2", ex3.Message);
			Assert.IsInstanceOfType(ex3, typeof(Exception));
			Assert.IsInstanceOfType(ex3, typeof(ServiceException));
		}
	}
}
