using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Refactora.Common.Extensions
{
	[TestClass]
	public class AsyncExtensionsTests
	{
		[TestMethod]
		public async Task SelectManyAsyncTests()
		{
			var test = new[]
			{
				new TestDto(new []{ "test", "test2" }),
				new TestDto(new []{ "test3", "test4" })
			};

			var result = (await test.SelectManyAsync(x => x.GetResultsAsync())).ToList();

			Assert.AreEqual("test", result[0]);
			Assert.AreEqual("test2", result[1]);
			Assert.AreEqual("test3", result[2]);
			Assert.AreEqual("test4", result[3]);
		}

		public class TestDto
		{
			private readonly IEnumerable<string> _values;

			public TestDto(IEnumerable<string> values)
			{
				_values = values;
			}

			public async Task<IEnumerable<string>> GetResultsAsync()
			{
				return await Task.FromResult(_values);
			}
		}
	}
}
