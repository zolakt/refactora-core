using System;
using System.Collections.Generic;

namespace Refactora.Common.Exceptions
{
	public class ServiceException : Exception
	{
		public ServiceException(string message) : base(message)
		{
			Errors = new[] { message };
		}

		public ServiceException(IEnumerable<string> messages) : base(string.Join(", ", messages))
		{
			Errors = messages;
		}

		public IEnumerable<string> Errors { get; }
	}
}