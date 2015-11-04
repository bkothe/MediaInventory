using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MediaInventory.Infrastructure.Framework.Linq
{
    public static class ExpressionExtensions
    {
        public class ExpressionMissingMethodException : Exception
        {
            public ExpressionMissingMethodException() :
                base("Expression must call a method.")
            { }
        }

        public class ExpressionMissingPropertyException : Exception
        {
            public ExpressionMissingPropertyException() :
                base("Expression must access a property.")
            { }
        }

        public static string GetMethodName<T>(this Expression<Action<T>> method)
        {
            if (!(method.Body is MethodCallExpression))
                throw new ExpressionMissingMethodException();
            return ((MethodCallExpression)method.Body).Method.Name;
        }

        public static string GetMethodName<T, TReturn>(this Expression<Func<T, TReturn>> method)
        {
            if (!(method.Body is MethodCallExpression))
                throw new ExpressionMissingMethodException();
            return ((MethodCallExpression)method.Body).Method.Name;
        }

        public static TResult GetPropertyValue<T, TResult>(this Expression<Func<T, TResult>> expression, T target)
        {
            var propertyInfo = expression.GetPropertyInfo();
            return (TResult)propertyInfo.GetValue(target, null);
        }

        public static PropertyInfo GetPropertyInfo<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            var memberExpression = expression.GetMemberExpression();
            var propertyInfo = memberExpression.Member as PropertyInfo;
            return propertyInfo;
        }

        public static string GetPropertyName<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            var propertyInfo = expression.GetPropertyInfo();
            return propertyInfo == null ? null : propertyInfo.Name;
        }

        public static string GetPropertyFullName<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            var propertyInfo = expression.GetPropertyInfo();
            return propertyInfo == null ? null : propertyInfo.DeclaringType.FullName + "." + propertyInfo.Name;
        }

        public static Expression<Func<TParam, object>> ToObjectReturn<TParam, TReturn>(this Expression<Func<TParam, TReturn>> field)
        {
            var body = field.Body.Type.IsValueType ? Expression.Convert(field.Body, typeof(object)) : field.Body;
            return Expression.Lambda<Func<TParam, object>>(body, field.Parameters);
        }

        public static MemberExpression GetMemberExpression<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            MemberExpression memberExpression = null;
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression.Body;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }
            if (memberExpression == null)
            {
                throw new ArgumentException("Not a member access", "expression");
            }
            return memberExpression;
        }
    }

}
