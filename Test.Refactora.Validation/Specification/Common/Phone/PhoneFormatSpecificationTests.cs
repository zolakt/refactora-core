using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Validation.Specification.Common.Phone;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Specification.Common.Phone
{
	[TestClass]
	public class PhoneFormatSpecificationTests
	{
		[TestMethod]
		public async Task PhoneFormatValidationTest()
		{
			var entities = new[] {
				new TestDto{ Phone = "(+351) 282 43 50 50" },
				new TestDto { Phone = "90191919908" },
				new TestDto { Phone = "555-8909" },
				new TestDto { Phone = "001 6867684" },
				new TestDto { Phone = "001 6867684x1" },
				new TestDto { Phone = "1 (234) 567-8901" },
				new TestDto { Phone = "1-234-567-8901 x1234" },
				new TestDto { Phone = "1-234-567-8901 ext1234" },
				new TestDto { Phone = "1-234 567.89/01 ext.1234" },
				new TestDto { Phone = "1(234)5678901x1234" },
				new TestDto { Phone = "(123)8575973" },
				new TestDto { Phone = "(0055)(123)8575973" },
				new TestDto { Phone = "+395989332656" },
				new TestDto { Phone = "0989332656" }
			};

			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[0])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[1])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[2])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[3])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[4])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[5])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[6])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[7])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[8])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[9])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[10])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[11])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[12])).Any());
			Assert.IsFalse((await new PhoneFormatSpecification<TestDto>(x => x.Phone).GetBrokenRulesAsync(entities[13])).Any());
		}

		public class TestDto
		{
			public string Phone { get; set; }
		}
	}
}
