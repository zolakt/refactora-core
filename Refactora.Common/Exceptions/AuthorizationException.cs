using System.Collections.Generic;

namespace Refactora.Common.Exceptions
{
	public class AuthorizationException : ServiceException
	{
		public const string DEFAULT_MESSAGE = "exception.authorization";

		public AuthorizationException() : base(DEFAULT_MESSAGE) { }

		public AuthorizationException(string message) : base(message) { }

		public AuthorizationException(IEnumerable<string> messages) : base(string.Join(", ", messages)) { }
	}
}