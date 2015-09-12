// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using System.Reflection;

namespace Jyuch.ReflectionToStringBuilder
{
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

        public ToStringPropertyMap Name(string name)
        {
            _name = name;
            return this;
        }
        
        public ToStringPropertyMap Ignore()
        {
            _isIgnore = true;
            return this;
        }

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
