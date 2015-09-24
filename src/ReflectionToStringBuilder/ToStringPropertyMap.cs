// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using System.Reflection;

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// Mapping info for a member to string-format.
    /// </summary>
    public sealed class ToStringPropertyMap
    {
        private readonly Func<object, object> _accessor;
        private string _name;

        private bool _isIgnore;
        internal bool IsIgnore
        {
            get
            {
                return _isIgnore;
            }
        }

        internal ToStringPropertyMap(Type objectType, MemberInfo member)
        {
            _accessor = ReflectionHelper.GetMemberAccessor(objectType, member);
            _name = member.Name;
            _isIgnore = false;
        }

        /// <summary>
        /// Set name to member.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The reference mapping for the member.</returns>
        public ToStringPropertyMap Name(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Ignore the member when generate string-format.
        /// </summary>
        /// <returns>The reference mapping for the member.</returns>
        public ToStringPropertyMap Ignore()
        {
            _isIgnore = true;
            return this;
        }

        /// <summary>
        /// Ignore the member when generate string-format.
        /// </summary>
        /// <param name="ignore">True to ignore, otherwise false.</param>
        /// <returns>The reference mapping for the member.</returns>
        public ToStringPropertyMap Ignore(bool ignore)
        {
            _isIgnore = ignore;
            return this;
        }

        internal string ToStringMember(object obj)
        {
            return $"{_name}={_accessor(obj)?.ToString() ?? string.Empty}";
        }
    }
}
