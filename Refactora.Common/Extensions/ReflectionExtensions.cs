using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Refactora.Common.Extensions
{
	public static class ReflectionExtensions
	{
		public static PropertyInfo GetPropertyInfo<TObjectType, TFieldType>(this Expression<Func<TObjectType, TFieldType>> expression)
		{
			var body = expression.Body;

			while (body.NodeType == ExpressionType.Convert || body.NodeType == ExpressionType.ConvertChecked)
			{
				body = ((UnaryExpression)body).Operand;
			}

			return (body as MemberExpression)?.Member as PropertyInfo ?? null;
		}

		public static string GetPropertyName<TObjectType, TFieldType>(this Expression<Func<TObjectType, TFieldType>> expression)
		{
			var property = GetPropertyInfo(expression);
			return property.Name;
		}

		public static object GetPropertyValue<TObjectType, TFieldType>(this Expression<Func<TObjectType, TFieldType>> expression, TObjectType target)
		{
			var property = GetPropertyInfo(expression);
			return property.GetValue(target);
		}
	}
}
