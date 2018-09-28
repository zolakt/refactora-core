using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactora.Validation.Exporter;
using Refactora.Validation.Specification.Common.Required;
using Refactora.Validation.Validator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Exporter
{
	[TestClass]
	public class JqueryValidationExporterTests
	{
		[TestMethod]
		public async Task JqueryValidationExporterExportTest()
		{
			var validator = new Mock<IValidator>();
			validator.Setup(x => x.AvailableSpecifications).Returns(new[]
			{
				new RequiredSpecification<TestDto>(x => x.Name),
				new RequiredSpecification<TestDto>(x => x.Description, "Desc empty", "desc")
			});

			var exporter = new JqueryValidationExporter();
			var result = (await exporter.ExportValidationRules(validator.Object)) as IDictionary<string, IDictionary<string, object>>;

			Assert.IsNotNull(result);
			Assert.AreEqual("required", result.Keys.FirstOrDefault());
			Assert.AreEqual("Name", result["required"].Keys.FirstOrDefault());
			Assert.AreEqual("desc", result["required"].Keys.LastOrDefault());
			Assert.AreEqual("Desc empty", result["required"]["desc"]);
		}

		public class TestDto
		{
			public string Name { get; set; }

			public string Description { get; set; }
		}
	}
}
