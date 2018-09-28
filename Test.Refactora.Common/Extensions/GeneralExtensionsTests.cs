using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Common.Extensions;
using System;

namespace Test.Refactora.Common.Extensions
{
	[TestClass]
	public class GeneralExtensionsTests
	{
		[TestMethod]
		public void IsNullOrEmptyTests()
		{
			Assert.IsFalse("test".IsNullOrEmpty());
			Assert.IsTrue("".IsNullOrEmpty());
			Assert.IsTrue((null as string).IsNullOrEmpty());
			Assert.IsTrue((null as object).IsNullOrEmpty());
			Assert.IsTrue((null as DateTime?).IsNullOrEmpty());
			Assert.IsFalse(DateTime.Now.IsNullOrEmpty());
		}

		[TestMethod]
		public void NullableCompareTests()
		{
			var test = 2 as int?;
			Assert.AreEqual(-1, test.Compare(5));
			Assert.AreEqual(1, test.Compare(1));
			Assert.AreEqual(0, test.Compare(2));
			Assert.AreEqual(1, test.Compare(null));

			var test2 = null as int?;
			Assert.AreEqual(-1, test2.Compare(5));
			Assert.AreEqual(-1, test2.Compare(1));
			Assert.AreEqual(-1, test2.Compare(2));
			Assert.AreEqual(0, test2.Compare(null));
		}
	}
}
