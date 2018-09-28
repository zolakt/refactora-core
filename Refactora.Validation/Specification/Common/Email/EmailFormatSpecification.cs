using Refactora.Validation.Specification.Common.Regex;
using System;
using System.Linq.Expressions;

namespace Refactora.Validation.Specification.Common.Email
{
	public class EmailFormatSpecification<TEntityType> : RegexSpecification<TEntityType>
	{
		private const string EMAIL_REGEX = 
			@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

		public EmailFormatSpecification(Expression<Func<TEntityType, string>> field, string description = null, string tag = null) :
			this(field, description, !string.IsNullOrEmpty(tag) ? new [] { tag } : new string[] { }) { }

		public EmailFormatSpecification(Expression<Func<TEntityType, string>> field, string description, string[] tags) :
			base(field, EMAIL_REGEX, description, tags)	{ }
	}
}
