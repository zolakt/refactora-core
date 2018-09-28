using System.Collections.Generic;

namespace Refactora.Common.Exceptions
{
	public class NotFoundException : ServiceException
	{
		public const string DEFAULT_MESSAGE = "exception.not_found";

		public NotFoundException() : base(DEFAULT_MESSAGE) { }

		public NotFoundException(string message) : base(message) { }

		public NotFoundException(IEnumerable<string> messages) : base(messages) { }
	}
}