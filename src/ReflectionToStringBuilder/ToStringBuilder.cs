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
            return ToString(obj, new ToStringConfig<T>());
        }

        public static string ToString<T>(T obj, ToStringConfig<T> config)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (config == null) throw new ArgumentNullException(nameof(config));

            var objType = typeof(T);
            IEnumerable<PropertyAccessor> exprs;
            if (!_cache.TryGetValue(objType, out exprs))
            {
                exprs = InitAccessor(objType);
                _cache.TryAdd(objType, exprs);
            }

            var displayExpr = exprs.Where(it => !config.IgnoreProperty.Any(j => j == it.PropertyInfo));
            var r = displayExpr.Select(it => new { PropertyName = it.PropertyInfo.Name, Value = it.Accessor(obj)});

            if (config.IgnoreMode != IgnorePropertyMode.None)
                r = r.Where(it => it.Value != null);

            if (config.IgnoreMode == IgnorePropertyMode.NullOrWhiteSpace)
                r = r.Where(it => !string.IsNullOrWhiteSpace(it.Value.ToString()));

            var toStringText = new StringBuilder();
            toStringText.Append(objType.Name).Append("{");

            if (r.Count() != 0)
            {
                var a = r.First();
                toStringText.Append(PropertyFormatter(a.PropertyName, a.Value));

                foreach (var it in r.Skip(1))
                {
                    toStringText.Append(",");
                    toStringText.Append(PropertyFormatter(it.PropertyName, it.Value));
                }
            }

            toStringText.Append("}");
            return toStringText.ToString();
        }

        private static string PropertyFormatter(string propertyName, object value)
        {
            return $"{propertyName}={value?.ToString() ?? string.Empty}";
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
    }
}
