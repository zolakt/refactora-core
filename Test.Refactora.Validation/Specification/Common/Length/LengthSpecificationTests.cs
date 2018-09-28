using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Validation.Specification.Common.Length;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Specification.Common.Length
{
	[TestClass]
	public class LengthSpecificationTests
	{
		[TestMethod]
		public async Task LengthStringTest()
		{
			var test1 = new TestDto
			{
				Name = "test"
			};

			var test2 = new TestDto();

			Assert.IsFalse((await new LengthSpecification<TestDto>(x => x.Name, 0, 4).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new LengthSpecification<TestDto>(x => x.Name, 0, 3).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new LengthSpecification<TestDto>(x => x.Name, 0, 10).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new LengthSpecification<TestDto>(x => x.Name, 4, 4).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new LengthSpecification<TestDto>(x => x.Name, 4, 6).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new LengthSpecification<TestDto>(x => x.Name, 5, 6).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new LengthSpecification<TestDto>(x => x.Name, 0, int.MaxValue).GetBrokenRulesAsync(test2)).Any());
			Assert.IsTrue((await new LengthSpecification<TestDto>(x => x.Name, 1, int.MaxValue).GetBrokenRulesAsync(test2)).Any());
		}

		[TestMethod]
		public async Task LengthArrayTest()
		{
			var test1 = new TestDto
			{
				Members = new[] { "test1", "test2" }
			};

			var test2 = new TestDto();

			Assert.IsFalse((await new LengthSpecification<TestDto>(x => x.Members, 0, 5).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new LengthSpecification<TestDto>(x => x.Members, 0, 2).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new LengthSpecification<TestDto>(x => x.Members, 0, 1).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new LengthSpecification<TestDto>(x => x.Members, 2, 3).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new LengthSpecification<TestDto>(x => x.Members, 3, 4).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new LengthSpecification<TestDto>(x => x.Members, 0, int.MaxValue).GetBrokenRulesAsync(test2)).Any());
			Assert.IsTrue((await new LengthSpecification<TestDto>(x => x.Members, 1, int.MaxValue).GetBrokenRulesAsync(test2)).Any());
		}

		public class TestDto
		{
			public string Name { get; set; }

			public string[] Members { get; set; }
		}
	}
}
