using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Validation.Specification;
using Refactora.Validation.Specification.Common.Required;
using Refactora.Validation.Validator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Validator
{
	[TestClass]
	public class SpecificationValidatorTests
	{
		[TestMethod]
		public async Task IsValidNoRulesTest()
		{
			var validator = new SpecificationValidator<TestDto>(new List<ISpecification<TestDto>>());
			var test = new TestDto();
			Assert.IsTrue(await validator.IsValidAsync(test));
		}

		[TestMethod]
		public async Task IsValidRequiredTest()
		{
			var validator = new SpecificationValidator<TestDto>(new []
			{
				new RequiredSpecification<TestDto>(x => x.Name)
			});

			var test = new TestDto();
			Assert.IsFalse(await validator.IsValidAsync(test));

			test.Name = "test";
			Assert.IsTrue(await validator.IsValidAsync(test));
		}

		[TestMethod]
		public async Task GetBrokenRulesTests()
		{
			var validator = new SpecificationValidator<TestDto>(new []
			{
				new RequiredSpecification<TestDto>(x => x.Name, "validation.name_required")
			});

			var test = new TestDto();
			var rules = await validator.GetBrokenRulesAsync(test);

			Assert.AreEqual(1, rules.Count());

			var first = rules.FirstOrDefault();
			Assert.AreEqual("validation.name_required", first.Description);
		}

		public class TestDto
		{
			public string Name { get; set; }
		}
	}
}
