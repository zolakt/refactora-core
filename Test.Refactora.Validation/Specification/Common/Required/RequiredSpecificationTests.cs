using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Validation.Specification.Common.Required;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Specification.Common.Required
{
	[TestClass]
	public class RequiredSpecificationTests
	{
		[TestMethod]
		public async Task RequiredStringTest()
		{
			var input = new TestDto
			{
				Id = "123"
			};

			var input2 = new TestDto();

			Assert.IsFalse((await new RequiredSpecification<TestDto>(x => x.Id).GetBrokenRulesAsync(input)).Any());
			Assert.IsTrue((await new RequiredSpecification<TestDto>(x => x.Id).GetBrokenRulesAsync(input2)).Any());
		}

		[TestMethod]
		public async Task RequiredObjectTest()
		{
			var input = new TestDto
			{
				Date = DateTime.Now
			};

			var input2 = new TestDto();

			Assert.IsFalse((await new RequiredSpecification<TestDto>(x => x.Date).GetBrokenRulesAsync(input)).Any());
			Assert.IsTrue((await new RequiredSpecification<TestDto>(x => x.Date).GetBrokenRulesAsync(input2)).Any());
		}

		[TestMethod]
		public async Task RequiredArrayTest()
		{
			var input = new TestDto
			{
				Tags = new[] { "test1", "test2" },
				Tags2 = new[] { "test1", "test2" }
			};

			var input2 = new TestDto();

			Assert.IsFalse((await new RequiredSpecification<TestDto>(x => x.Tags).GetBrokenRulesAsync(input)).Any());
			Assert.IsFalse((await new RequiredSpecification<TestDto>(x => x.Tags2).GetBrokenRulesAsync(input)).Any());

			Assert.IsTrue((await new RequiredSpecification<TestDto>(x => x.Tags).GetBrokenRulesAsync(input2)).Any());
			Assert.IsTrue((await new RequiredSpecification<TestDto>(x => x.Tags2).GetBrokenRulesAsync(input2)).Any());
		}

		public class TestDto
		{
			public string Id { get; set; }

			public DateTime? Date { get; set; }

			public string[] Tags { get; set; }

			public IEnumerable<string> Tags2 { get; set; }
		}
	}
}
