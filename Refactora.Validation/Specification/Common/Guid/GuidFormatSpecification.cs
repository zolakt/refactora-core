using Refactora.Validation.Specification.Common.Regex;
using System;
using System.Linq.Expressions;

namespace Refactora.Validation.Specification.Common.Guid
{
	public class GuidFormatSpecification<TEntityType> : RegexSpecification<TEntityType>
	{
		private const string GUID_REGEX = @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$";

		public GuidFormatSpecification(Expression<Func<TEntityType, string>> field, string description = null, string tag = null) :
			this(field, description, !string.IsNullOrEmpty(tag) ? new [] { tag } : new string[] { }) { }

		public GuidFormatSpecification(Expression<Func<TEntityType, string>> field, string description, string[] tags) :
			base(field, GUID_REGEX, description, tags)	{ }
	}
}
