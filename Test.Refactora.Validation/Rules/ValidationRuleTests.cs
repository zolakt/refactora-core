using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Common.Mapper;
using Refactora.Validation.Rules;
using System.Linq;

namespace Test.Refactora.Validation.Rules
{
	[TestClass]
	public class ValidationRuleTests
	{
		[TestMethod]
		public void ValidationRuleConstructorTests()
		{
			var rule1 = new ValidationRule("test");
			Assert.AreEqual("test", rule1.Description);
			Assert.IsInstanceOfType(rule1, typeof(IBusinessRule));

			var rule2 = new ValidationRule("test", "test2");
			Assert.AreEqual("test", rule2.Description);
			Assert.AreEqual("test2", rule2.Tags.FirstOrDefault());
			Assert.IsInstanceOfType(rule2, typeof(IBusinessRule));

			var rule3 = new ValidationRule("test", new[] { "test2", "test3" });
			Assert.AreEqual("test", rule3.Description);
			Assert.AreEqual("test2", rule3.Tags.FirstOrDefault());
			Assert.AreEqual("test3", rule3.Tags.LastOrDefault());
			Assert.IsInstanceOfType(rule3, typeof(IBusinessRule));
		}
	}
}
