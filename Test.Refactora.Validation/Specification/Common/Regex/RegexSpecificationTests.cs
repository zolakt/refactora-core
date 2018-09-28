using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Validation.Specification.Common.Regex;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Specification.Common.Regex
{
	[TestClass]
	public class RegexSpecificationTests
	{
		[TestMethod]
		public async Task RegexValidationTest()
		{
			var regex = @"^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$";

			var entities = new[] {
				new TestDto{ Name = "(+351) 282 43 50 50" },
				new TestDto { Name = "90191919908" },
				new TestDto { Name = "555-8909" },
				new TestDto { Name = "001 6867684" },
				new TestDto { Name = "001 6867684x1" },
				new TestDto { Name = "1 (234) 567-8901" },
				new TestDto { Name = "1-234-567-8901 x1234" },
				new TestDto { Name = "1-234-567-8901 ext1234" },
				new TestDto { Name = "1-234 567.89/01 ext.1234" },
				new TestDto { Name = "1(234)5678901x1234" },
				new TestDto { Name = "(123)8575973" },
				new TestDto { Name = "(0055)(123)8575973" },
				new TestDto { Name = "+395989332656" },
				new TestDto { Name = "0989332656" }
			};

			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[0])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[1])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[2])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[3])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[4])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[5])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[6])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[7])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[8])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[9])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[10])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[11])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[12])).Any());
			Assert.IsFalse((await new RegexSpecification<TestDto>(x => x.Name, regex).GetBrokenRulesAsync(entities[13])).Any());
		}

		public class TestDto
		{
			public string Name { get; set; }
		}
	}
}
