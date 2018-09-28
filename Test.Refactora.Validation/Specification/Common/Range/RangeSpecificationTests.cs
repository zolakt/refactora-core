using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Validation.Specification.Common.Range;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Specification.Common.Range
{
	[TestClass]
	public class RangeSpecificationTests
	{
		[TestMethod]
		public async Task RangeNumericTest()
		{
			var test1 = new TestDto
			{
				Value1 = 100,
				Value2 = 100
			};

			var test2 = new TestDto();

			Assert.IsFalse((await new RangeSpecification<TestDto, int>(x => x.Value1, 50, 200).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new RangeSpecification<TestDto, int>(x => x.Value1, 100, 100).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new RangeSpecification<TestDto, int>(x => x.Value1, 50, 80).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new RangeSpecification<TestDto, int>(x => x.Value1, 0, 90).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new RangeSpecification<TestDto, int>(x => x.Value1, 0, 500).GetBrokenRulesAsync(test2)).Any());

			Assert.IsFalse((await new RangeSpecification<TestDto, decimal>(x => x.Value2, 50, 200).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new RangeSpecification<TestDto, decimal>(x => x.Value2, 100, 100).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new RangeSpecification<TestDto, decimal>(x => x.Value2, 50, 80).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new RangeSpecification<TestDto, decimal>(x => x.Value2, 0, 90).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new RangeSpecification<TestDto, decimal>(x => x.Value2, 0, 500).GetBrokenRulesAsync(test2)).Any());
			Assert.IsTrue((await new RangeSpecification<TestDto, decimal>(x => x.Value2, 1, 500).GetBrokenRulesAsync(test2)).Any());
		}

		[TestMethod]
		public async Task RangeDateTimeTest()
		{
			var now = DateTime.Now;

			var test1 = new TestDto
			{
				Date = now
			};

			var test2 = new TestDto();


			Assert.IsFalse((await new RangeSpecification<TestDto, DateTime>(x => x.Date, now.AddHours(-1), now.AddHours(1)).GetBrokenRulesAsync(test1)).Any());
			Assert.IsFalse((await new RangeSpecification<TestDto, DateTime>(x => x.Date, now, now).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new RangeSpecification<TestDto, DateTime>(x => x.Date, now.AddHours(1), now.AddHours(2)).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new RangeSpecification<TestDto, DateTime>(x => x.Date, now.AddHours(-2), now.AddHours(-1)).GetBrokenRulesAsync(test1)).Any());
			Assert.IsTrue((await new RangeSpecification<TestDto, DateTime>(x => x.Date, now.AddHours(-20), now.AddHours(20)).GetBrokenRulesAsync(test2)).Any());
		}

		public class TestDto
		{
			public int? Value1 { get; set; }

			public decimal Value2 { get; set; }

			public DateTime? Date { get; set; }
		}
	}
}
