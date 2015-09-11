// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jyuch.ReflectionToStringBuilder
{
    public sealed class ToStringPropertyMap
    {
        private readonly PropertyInfo _property;
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

        internal ToStringPropertyMap(Type objectType, PropertyInfo property)
        {
            _property = property;
            _accessor = ReflectionHelper.GetMemberAccessor(objectType, property);
            _name = _property.Name;
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
    }
}
