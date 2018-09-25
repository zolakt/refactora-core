using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactora.Auth.Common;

namespace Test.Refactora.Auth.Common
{
	[TestClass]
	public class ContactDetailsTests
	{
		[TestMethod]
		public void ContactDetailsContructorTest()
		{
			var firstName = "a";
			var lastName = "b";

			var test = new ContactDetails
			{
				FirstName = firstName,
				LastName = lastName
			};
			Assert.AreEqual(test.FullName, string.Format("{0} {1}", firstName, lastName));

			var test2 = new ContactDetails
			{
				FirstName = firstName
			};
			Assert.AreEqual(test2.FullName, firstName);

			var test3 = new ContactDetails
			{
				LastName = lastName
			};
			Assert.AreEqual(test3.FullName, lastName);

			var test4 = new ContactDetails();
			Assert.AreEqual(test4.FullName, string.Empty);
		}
	}
}
