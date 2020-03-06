using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.Extensions
{
    public static class ExpressionBuilder
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains");
        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static readonly MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        private static MemberExpression GetExpressionBody<TSource>(ParameterExpression param, string propertyName)
        {

            Expression body = param;

            string[] arr = propertyName.Split('.');
            if (arr.Length > 1)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    body = Expression.PropertyOrField(body, arr[i]);
                }
            }
            else
            {
                body = Expression.PropertyOrField(body, arr[0]);
            }

            return body as MemberExpression;
        }

        public static Expression<Func<TSource, bool>> GetSearchExpression<TSource>(IList<SearchInfo> searchableColums, string searchValue)
        {
            List<Expression> expressions = new List<Expression>();
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "t");
            foreach (var searchInfo in searchableColums)
            {
                MethodInfo searchMethodInfo = typeof(string).GetMethod(searchInfo.operation.ToString(), new[] { typeof(string),typeof(StringComparison) });
                MemberExpression memberExpression = GetExpressionBody<TSource>(parameter, searchInfo.propertyName);
                ConstantExpression constantExpression = Expression.Constant(searchValue, typeof(string));
                MethodCallExpression methodCallExpression = Expression.Call(memberExpression, searchMethodInfo, constantExpression, Expression.Constant(StringComparison.InvariantCultureIgnoreCase));
                expressions.Add(methodCallExpression);
            }
            if (expressions.Count == 0)
                return null;
            var orExpression = expressions[0];
            for (var i = 1; i < expressions.Count; i++)
            {
                orExpression = Expression.OrElse(orExpression, expressions[i]);
            }
            Expression<Func<TSource, bool>> expression = Expression.Lambda<Func<TSource, bool>>(
                orExpression, parameter);
            return expression;
        }

        private class ExpressionParameterReplacer : ExpressionVisitor
        {
            public ExpressionParameterReplacer(IList<ParameterExpression> fromParameters, IList<ParameterExpression> toParameters)
            {
                ParameterReplacements = new Dictionary<ParameterExpression, ParameterExpression>();
                for (int i = 0; i != fromParameters.Count && i != toParameters.Count; i++)
                    ParameterReplacements.Add(fromParameters[i], toParameters[i]);
            }

            private IDictionary<ParameterExpression, ParameterExpression> ParameterReplacements { get; set; }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                ParameterExpression replacement;
                if (ParameterReplacements.TryGetValue(node, out replacement))
                    node = replacement;
                return base.VisitParameter(node);
            }
        }

        public static object Evaluation<TModel>(TModel model,string column_Property_Exp)
        {
            string columnStr = column_Property_Exp.Substring(column_Property_Exp.IndexOf('.')).TrimStart('.');
            return GetPropertyValue(model, columnStr);
        }

        private static object GetPropertyValue(Object fromObject, string propertyName)
        {
            Type objectType = fromObject.GetType();
            PropertyInfo propInfo = objectType.GetProperty(propertyName);
            if (propInfo == null && propertyName.Contains('.'))
            {
                string firstProp = propertyName.Substring(0, propertyName.IndexOf('.'));
                propInfo = objectType.GetProperty(firstProp);
                if (propInfo == null)//property name is invalid
                {
                    throw new ArgumentException(String.Format("Property {0} is not a valid property of {1}.", firstProp, fromObject.GetType().ToString()));
                }
                return GetPropertyValue(propInfo.GetValue(fromObject, null), propertyName.Substring(propertyName.IndexOf('.') + 1));
            }
            else
            {
                return propInfo.GetValue(fromObject, null);
            }
        }

    }
}
