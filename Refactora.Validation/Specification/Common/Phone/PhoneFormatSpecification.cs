using Refactora.Validation.Specification.Common.Regex;
using System;
using System.Linq.Expressions;

namespace Refactora.Validation.Specification.Common.Phone
{
	public class PhoneFormatSpecification<TEntityType> : RegexSpecification<TEntityType>
	{
		private const string PHONE_REGEX = @"^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$";

		public PhoneFormatSpecification(Expression<Func<TEntityType, string>> field, string description = null, string tag = null) :
			this(field, description, !string.IsNullOrEmpty(tag) ? new [] { tag } : new string[] { }) { }

		public PhoneFormatSpecification(Expression<Func<TEntityType, string>> field, string description, string[] tags) :
			base(field, PHONE_REGEX, description, tags)	{ }
	}
}
