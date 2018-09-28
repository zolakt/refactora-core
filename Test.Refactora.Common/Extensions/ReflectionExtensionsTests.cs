using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Test.Refactora.Common.Extensions
{
	[TestClass]
	public class ReflectionExtensionsTests
	{
		[TestMethod]
		public void GetPropertyInfoTests()
		{
			Expression<Func<TestDto, string>> exp1 = (x) => x.Name;
			Assert.AreEqual("Name", exp1.GetPropertyInfo().Name);

			Expression<Func<TestDto, DateTime?>> exp2 = (x) => x.Date;
			Assert.AreEqual("Date", exp2.GetPropertyInfo().Name);

			Expression<Func<TestDto, IEnumerable<string>>> exp3 = (x) => x.Items;
			Assert.AreEqual("Items", exp3.GetPropertyInfo().Name);
		}

		[TestMethod]
		public void GetPropertyNameTests()
		{
			Expression<Func<TestDto, string>> exp1 = (x) => x.Name;
			Assert.AreEqual("Name", exp1.GetPropertyName());

			Expression<Func<TestDto, DateTime?>> exp2 = (x) => x.Date;
			Assert.AreEqual("Date", exp2.GetPropertyName());

			Expression<Func<TestDto, IEnumerable<string>>> exp3 = (x) => x.Items;
			Assert.AreEqual("Items", exp3.GetPropertyName());
		}

		[TestMethod]
		public void GetPropertyValueTests()
		{
			var test = new TestDto
			{
				Name = "test",
				Date = DateTime.Now,
				Items = new[] { "test2", "test3" }
			};

			Expression<Func<TestDto, string>> exp1 = (x) => x.Name;
			Assert.AreEqual(test.Name, exp1.GetPropertyValue(test));

			Expression<Func<TestDto, DateTime?>> exp2 = (x) => x.Date;
			Assert.AreEqual(test.Date, exp2.GetPropertyValue(test));

			Expression<Func<TestDto, IEnumerable<string>>> exp3 = (x) => x.Items;
			Assert.AreEqual(test.Items, exp3.GetPropertyValue(test));
		}

		public class TestDto
		{
			public string Name { get; set; }

			public DateTime? Date { get; set; }

			public IEnumerable<string> Items { get; set; }
		}
	}
}
