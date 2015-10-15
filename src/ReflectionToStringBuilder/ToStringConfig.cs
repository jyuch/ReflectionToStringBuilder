// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// Represent configuration of generate string-format.
    /// </summary>
    /// <typeparam name="T">The type of object generate a string-format.</typeparam>
    public sealed class ToStringConfig<T>
    {
        private HashSet<MemberInfo> _ignoreMember = new HashSet<MemberInfo>();

        /// <summary>
        /// Specify mode of ignoring member. Default is <see cref="IgnoreMemberMode.None"/>.
        /// </summary>
        public IgnoreMemberMode IgnoreMode { get; set; } = IgnoreMemberMode.None;

        /// <summary>
        /// Specify output target member. Default is <see cref="TargetType.Property"/>.
        /// </summary>
        public TargetType OutputTarget { get; set; } = TargetType.Property;

        public bool ExpandIEnumerable { get; set; } = false;

        /// <summary>
        /// Specify member of ignoring when generate string-format.
        /// </summary>
        /// <param name="expression">The member of ignoring.</param>
        public void SetIgnoreMember(Expression<Func<T, object>> expression)
        {
            _ignoreMember.Add(ReflectionHelper.GetMember(expression));
        }

        internal HashSet<MemberInfo> IgnoreMember
        {
            get
            {
                return _ignoreMember;
            }
        }
    }

    internal class DefaultConfig<T>
    {
        internal static readonly ToStringConfig<T> Value = new ToStringConfig<T>();
    }
}
