using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Jyuch.ReflectionToStringBuilder
{
    public sealed class ToStringConfig<T>
    {
        private HashSet<PropertyInfo> _ignoreProperty = new HashSet<PropertyInfo>();

        public IgnorePropertyMode IgnoreMode { get; set; } = IgnorePropertyMode.None;

        internal HashSet<PropertyInfo> IgnoreProperty
        {
            get
            {
                return _ignoreProperty;
            }
        }

        public void SetIgnoreProperty(Expression<Func<T, object>> expression)
        {
            _ignoreProperty.Add(GetProperty(expression));
        }
        
        // 以下の2つのメソッドは CsvHelper.ReflectionHelper のメソッドを改変して使用しています
        // Copyright 2009-2015 Josh Close and Contributors
        // This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
        // See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
        // http://csvhelper.com
        private static PropertyInfo GetProperty<TModel>(Expression<Func<TModel, object>> expression)
        {
            var member = GetMemberExpression(expression).Member;
            var property = member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException(string.Format("'{0}' is not a property. Did you try to map a field by accident?", member.Name));
            }

            return property;
        }

        private static MemberExpression GetMemberExpression<TModel, TResult>(Expression<Func<TModel, TResult>> expression)
        {
            // This method was taken from FluentNHibernate.Utils.ReflectionHelper.cs and modified.
            // http://fluentnhibernate.org/

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
