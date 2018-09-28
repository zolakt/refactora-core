using System.Collections.Generic;

namespace Refactora.Common.Exceptions
{
	public class ValidationException : ServiceException
	{
		public const string DEFAULT_MESSAGE = "exception.validation";

		public ValidationException() : base(DEFAULT_MESSAGE) { }

		public ValidationException(string message) : base(message) { }

		public ValidationException(IEnumerable<string> messages) : base(string.Join(", ", messages)) { }
	}
}