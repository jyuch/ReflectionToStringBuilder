﻿// Copyright (c) 2015-2016 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Reflection.BindingFlags;

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// Provides static method for generate string-format.
    /// </summary>
    public static class ToStringBuilder
    {
        private static ConcurrentDictionary<Type, IEnumerable<MemberAccessor>> _accessorCache
            = new ConcurrentDictionary<Type, IEnumerable<MemberAccessor>>();

        private class MemberAccessor
        {
            public MemberInfo MemberInfo { get; }
            public Func<object, object> Accessor { get; }

            public MemberAccessor(MemberInfo info, Func<object, object> accessor)
            {
                MemberInfo = info;
                Accessor = accessor;
            }
        }

        /// <summary>
        /// Generates string-format of specified object.
        /// </summary>
        /// <typeparam name="T">The type of object generate a string-format.</typeparam>
        /// <param name="obj">The instance of generate a string-format.</param>
        /// <returns>The string-format of instance.</returns>
        public static string ToString<T>(T obj)
        {
            return ToString(obj, DefaultConfig<T>.Value);
        }

        /// <summary>
        /// Generates string-format of specified object using configuration.
        /// </summary>
        /// <typeparam name="T">The type of object to generate a string-format.</typeparam>
        /// <param name="obj">The instance to generate a string-format.</param>
        /// <param name="config">The configuration for generate string-format.</param>
        /// <returns>The string-format of instance.</returns>
        public static string ToString<T>(T obj, ToStringConfig<T> config)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (config == null) throw new ArgumentNullException(nameof(config));

            var objType = typeof(T);
            IEnumerable<MemberAccessor> exprs;
            if (!_accessorCache.TryGetValue(objType, out exprs))
            {
                exprs = InitAccessor(objType);
                _accessorCache.TryAdd(objType, exprs);
            }

            var r = exprs
                .Where(it => !config.IgnoreMember.Contains(it.MemberInfo))
                .Where(it => config.OutputTarget != TargetType.Property || it.MemberInfo is PropertyInfo)
                .Where(it => config.OutputTarget != TargetType.Field || it.MemberInfo is FieldInfo)
                .Select(it => new { MemberName = it.MemberInfo.Name, Value = it.Accessor(obj) })
                .Where(it => config.IgnoreMode == IgnoreMemberMode.None || it.Value != null)
                .Where(it => config.IgnoreMode != IgnoreMemberMode.NullOrWhiteSpace || !string.IsNullOrWhiteSpace(it.Value.ToString()));

            var toStringText = new StringBuilder();
            toStringText.Append(objType.Name).Append("{");
            toStringText.Append(string.Join(",", r.Select(it => PropertyFormatter(it.MemberName, it.Value, config.ExpandIEnumerable))));
            toStringText.Append("}");
            return toStringText.ToString();
        }

        private static string PropertyFormatter(string propertyName, object value, bool isExpandEnum)
        {
            if (isExpandEnum && value is System.Collections.IEnumerable)
            {
                var e = ((System.Collections.IEnumerable)value).Cast<object>().Select(it => it?.ToString() ?? string.Empty);
                return $"{propertyName}=[{string.Join(",", e)}]";
            }
            else
            {
                return $"{propertyName}={value?.ToString() ?? string.Empty}";
            }
        }

        private static IEnumerable<MemberAccessor> InitAccessor(Type targetType)
        {
            return targetType.GetMembers(Public | Instance)
                .Where((it) => it is PropertyInfo || it is FieldInfo)
                .Where((it) => it is FieldInfo || ((PropertyInfo)it).GetIndexParameters().Length == 0)
                .Where((it) => it is FieldInfo || ((PropertyInfo)it).CanRead)
                .Select((it) => new MemberAccessor(it, ReflectionHelper.GetMemberAccessor(targetType, it)))
                .ToArray();
        }
    }
}
