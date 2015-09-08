// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// オブジェクトの文字列形式を返す静的メソッドを提供します。
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
        /// オブジェクトの文字列形式を動的に生成して返します。
        /// </summary>
        /// <typeparam name="T">文字列形式を生成するオブジェクトの型。</typeparam>
        /// <param name="obj">文字列形式を生成するインスタンス。</param>
        /// <returns>オブジェクトの文字列形式。</returns>
        public static string ToString<T>(T obj)
        {
            return ToString(obj, new ToStringConfig<T>());
        }

        /// <summary>
        /// 指定した設定を用いてオブジェクトの文字列形式を動的に生成して返します。
        /// </summary>
        /// <typeparam name="T">文字列形式を生成するオブジェクトの型。</typeparam>
        /// <param name="obj">文字列形式を生成するインスタンス。</param>
        /// <param name="config">文字列形式の生成に用いる設定。</param>
        /// <returns>オブジェクトの文字列形式。</returns>
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
                .Select(it => new { MemberName = it.MemberInfo.Name, Value = it.Accessor(obj) });
            
            if (config.IgnoreMode != IgnoreMemberMode.None)
                r = r.Where(it => it.Value != null);

            if (config.IgnoreMode == IgnoreMemberMode.NullOrWhiteSpace)
                r = r.Where(it => !string.IsNullOrWhiteSpace(it.Value.ToString()));

            var toStringText = new StringBuilder();
            toStringText.Append(objType.Name).Append("{");
            toStringText.Append(string.Join(",", r.Select(it => PropertyFormatter(it.MemberName, it.Value))));
            toStringText.Append("}");
            return toStringText.ToString();
        }

        private static string PropertyFormatter(string propertyName, object value)
        {
            return $"{propertyName}={value?.ToString() ?? string.Empty}";
        }

        private static IEnumerable<MemberAccessor> InitAccessor(Type targetType)
        {
            var toStringMember = targetType.GetMembers()
                .Where((it) => it is PropertyInfo || it is FieldInfo)
                .Where((it) => it is FieldInfo || ((PropertyInfo)it).GetIndexParameters().Length == 0)
                .Where((it) => it is FieldInfo || ((PropertyInfo)it).CanRead);
            
            var result = new List<MemberAccessor>();

            foreach (var it in toStringMember)
            {
                Func<object, object> expr = ReflectionHelper.GetMemberAccessor(targetType, it);
                result.Add(new MemberAccessor(it, expr));
            }

            return result;
        }
    }
}
