using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Validation.Specification.Common.Guid;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Specification.Common.Guid
{
	[TestClass]
	public class GuidFormatSpecificationTests
	{
		[TestMethod]
		public async Task GuidFormatValidationTest()
		{
			var entities = new[] {
				new TestDto{ Guid = "ca761232ed4211cebacd00aa0057b223" },
				new TestDto { Guid = "CA761232-ED42-11CE-BACD-00AA0057B223" }
			};

			Assert.IsTrue((await new GuidFormatSpecification<TestDto>(x => x.Guid).GetBrokenRulesAsync(entities[0])).Any());
			Assert.IsFalse((await new GuidFormatSpecification<TestDto>(x => x.Guid).GetBrokenRulesAsync(entities[1])).Any());
		}

		public class TestDto
		{
			public string Guid { get; set; }
		}
	}
}
