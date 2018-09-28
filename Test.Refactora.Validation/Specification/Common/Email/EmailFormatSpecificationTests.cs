using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Validation.Specification.Common.Email;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Specification.Common.Email
{
	[TestClass]
	public class EmailFormatSpecificationTests
	{
		[TestMethod]
		public async Task EmailFormatValidationTest()
		{
			var entities = new[] {
				new TestDto{ Email = "david.jones@proseware.com" },
				new TestDto { Email = "d.j@server1.proseware.com" },
				new TestDto { Email = "jones@ms1.proseware.com" },
				new TestDto { Email = "j.@server1.proseware.com" },
				new TestDto { Email = "j@proseware.com9" },
				new TestDto { Email = "js#internal@proseware.com" },
				new TestDto { Email = "j_9@[129.126.118.1]" },
				new TestDto { Email = "j..s@proseware.com" },
				new TestDto { Email = "js*@proseware.com" },
				new TestDto { Email = "js@proseware..com" },
				new TestDto { Email = "js@proseware.com9" },
				new TestDto { Email = "j.s@server1.proseware.com" },
				new TestDto { Email = "\"j\\\"s\\\"\"@proseware.com" },
				new TestDto { Email = "js@contoso.中国" }
			};

			Assert.IsFalse((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[0])).Any());
			Assert.IsFalse((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[1])).Any());
			Assert.IsFalse((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[2])).Any());
			Assert.IsTrue((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[3])).Any());
			Assert.IsFalse((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[4])).Any());
			Assert.IsFalse((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[5])).Any());
			Assert.IsFalse((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[6])).Any());
			Assert.IsTrue((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[7])).Any());
			Assert.IsTrue((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[8])).Any());
			Assert.IsTrue((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[9])).Any());
			Assert.IsFalse((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[10])).Any());
			Assert.IsFalse((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[11])).Any());
			Assert.IsFalse((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[12])).Any());
			Assert.IsTrue((await new EmailFormatSpecification<TestDto>(x => x.Email).GetBrokenRulesAsync(entities[13])).Any());
		}

		public class TestDto
		{
			public string Email { get; set; }
		}
	}
}
