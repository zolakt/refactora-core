using System;

namespace Test.Refactora.Auth.Fakes
{
	public class FakeUser
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string[] Permissions { get; set; }
	}
}