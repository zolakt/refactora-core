using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactora.Common.Exceptions;
using Refactora.Validation.Manager;
using Refactora.Validation.Validator;
using System.Threading.Tasks;

namespace Test.Refactora.Validation.Manager
{
	[TestClass]
	public class ValidationMangerTests
	{
		[TestMethod]
		public void RegisterValidatorTest()
		{
			var validator = new Mock<IValidator<TestDto>>();
			var validator2 = new Mock<IValidator<TestDto>>();

			var factory = new ValidationManger<DefaultValidatorTypes, TestDto>();
			factory.RegisterValidator(DefaultValidatorTypes.Get, validator.Object);
			factory.RegisterValidator(DefaultValidatorTypes.Insert, validator2.Object);

			Assert.IsTrue(true);
		}

		[TestMethod]
		public async Task CheckValidationValidTest()
		{
			var test = new TestDto
			{
				Name = "test"
			};

			var validator = new Mock<IValidator<TestDto>>();
			validator.Setup(x => x.ValidateAsync(test)).Returns(Task.FromResult(true)); ;

			var validator2 = new Mock<IValidator<TestDto>>();
			validator2.Setup(x => x.ValidateAsync(test)).Returns(Task.FromResult(true)); ;

			var manager = new ValidationManger<DefaultValidatorTypes, TestDto>();
			manager.RegisterValidator(DefaultValidatorTypes.Get, validator.Object);

			await manager.ValidateAsync(DefaultValidatorTypes.Get, test);
			Assert.IsTrue(true);
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceException), AllowDerivedTypes = true)]
		public async Task CheckValidationInvalidTest()
		{
			var test = new TestDto
			{
				Name = "test"
			};

			var validator = new Mock<IValidator<TestDto>>();
			validator.Setup(x => x.ValidateAsync(test)).Returns(Task.FromResult(true));

			var validator2 = new Mock<IValidator<TestDto>>();
			validator2.Setup(x => x.ValidateAsync(test)).ThrowsAsync(new ValidationException("test"));

			var manager = new ValidationManger<DefaultValidatorTypes, TestDto>();
			manager.RegisterValidator(DefaultValidatorTypes.Get, validator.Object);
			manager.RegisterValidator(DefaultValidatorTypes.Insert, validator2.Object);

			await manager.ValidateAsync(DefaultValidatorTypes.Get, test); // no exception

			await manager.ValidateAsync(DefaultValidatorTypes.Insert, test); //throws exception
		}

		public class TestDto
		{
			public string Name { get; set; }
		}
	}
}
