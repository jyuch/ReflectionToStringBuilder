using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Jyuch.ReflectionToStringBuilder
{
    public static class ToStringBuilder
    {
        private static ConcurrentDictionary<Type, IEnumerable<PropertyAccessor>> _cache
            = new ConcurrentDictionary<Type, IEnumerable<PropertyAccessor>>();

        private class PropertyAccessor
        {
            public PropertyInfo PropertyInfo { get; }
            public Func<object, object> Accessor { get; }

            public PropertyAccessor(PropertyInfo info, Func<object, object> accessor)
            {
                PropertyInfo = info;
                Accessor = accessor;
            }
        }

        public static string ToString<T>(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var objType = typeof(T);
            IEnumerable<PropertyAccessor> exprs;
            if (!_cache.TryGetValue(objType, out exprs))
            {
                exprs = InitAccessor(objType);
                _cache.TryAdd(objType, exprs);
            }

            var toStringText = new StringBuilder();
            toStringText.Append(objType.Name).Append("{");

            if (exprs.Count() != 0)
            {
                var a = exprs.First();
                toStringText.Append(PropertyFormatter(a, obj));

                foreach (var it in exprs.Skip(1))
                {
                    toStringText.Append(",");
                    toStringText.Append(PropertyFormatter(it, obj));
                }
            }

            toStringText.Append("}");
            return toStringText.ToString();
        }

        private static IEnumerable<PropertyAccessor> InitAccessor(Type targetType)
        {
            var toStringProp = targetType.GetProperties()
                .Where((it) => it.CanRead)
                .Where((it) => it.GetIndexParameters().Length == 0);

            var result = new List<PropertyAccessor>();

            foreach (var it in toStringProp)
            {
                // same as it => ((targetType)it).Property
                var arg = Expression.Parameter(typeof(object), "it");
                var convToTarget = Expression.Convert(arg, targetType);
                var getPropValue = Expression.MakeMemberAccess(convToTarget, it);
                var convToObject = Expression.Convert(getPropValue, typeof(object));
                var lambda = Expression.Lambda(convToObject, arg);
                Func<object, object> expr = (Func<object, object>)lambda.Compile();
                result.Add(new PropertyAccessor(it, expr));
            }

            return result;
        }

        private static string PropertyFormatter(PropertyAccessor p, object obj)
        {
            return $"{p.PropertyInfo.Name}={p.Accessor(obj)?.ToString() ?? string.Empty}";
        }
    }
}
