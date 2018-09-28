using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactora.Common.Mapper;

namespace Test.Refactora.Common.Mapper
{
	[TestClass]
	public class DataAutoMapperTests
	{
		[TestMethod]
		public void AutoMapperTest()
		{
			var initial = new TestDto1
			{
				Id = "test"
			};

			var expected = new TestDto2
			{
				ID = initial.Id
			};

			var expected2 = new TestDto2();

			var automapper = new Mock<IMapper>();
			automapper.Setup(x => x.Map<TestDto2>(initial)).Returns(expected);
			automapper.Setup(x => x.Map<TestDto1, TestDto2>(initial)).Returns(expected);
			automapper.Setup(x => x.Map(initial, expected2)).Returns(expected);

			var mapper = new DataAutoMapper(automapper.Object);

			var result1 = mapper.Map<TestDto2>(initial);
			Assert.AreEqual(expected.ID, result1.ID);
			automapper.Verify(x => x.Map<TestDto2>(initial), Times.Once);

			var result2 = mapper.Map<TestDto1, TestDto2>(initial);
			Assert.AreEqual(expected.ID, result2.ID);
			automapper.Verify(x => x.Map<TestDto2>(initial), Times.Exactly(2));

			var result3 = mapper.Map(initial, expected2);
			Assert.AreEqual(expected.ID, result3.ID);
			automapper.Verify(x => x.Map<TestDto2>(initial), Times.Exactly(2));
			automapper.Verify(x => x.Map(initial, expected2), Times.Once);
		}

		public class TestDto1
		{
			public string Id { get; set; }
		}

		public class TestDto2
		{
			public string ID { get; set; }
		}
	}
}
